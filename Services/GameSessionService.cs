using Microsoft.EntityFrameworkCore;
using PokerGamesRSF;
using PokerGamesRSF.Models;
using PokerGamesRSF.DTO;
using PokerGamesRSF.Models;
using PokerGamesRSF.Services;
using PokerGamesRSF;
using PokerGamesRSF.DTO;

namespace PokerGamesRSF.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly IDbContextFactory<MyDbContext> _contextFactory;

        public GameSessionService(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<GameSession> StartGameAsync(Guid player1Id, Guid player2Id)
        {
            using var context = _contextFactory.CreateDbContext();
            var session = new GameSession
            {
                Player1Id = player1Id,
                Player2Id = player2Id,
                Phase = GamePhase.PreFlop,
                Pot = 0,
                CurrentBet = 0,
                IsActive = true,
                LastActionTime = DateTime.UtcNow
            };
            // Randomly select starting player
            var rnd = new Random();
            session.CurrentTurnPlayerId = (rnd.Next(2) == 0) ? player1Id : player2Id;

            await context.GameSessions.AddAsync(session);
            await context.SaveChangesAsync();

            // Shuffle deck and deal cards
            var deck = await context.Cards.ToListAsync();
            deck = deck.OrderBy(c => Guid.NewGuid()).ToList();
            // Deal pocket cards
            var player1Cards = deck.Take(2).ToList();
            var player2Cards = deck.Skip(2).Take(2).ToList();
            foreach (var card in player1Cards)
            {
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    UserId = player1Id,
                    CardId = card.Id,
                    IsInHand = true
                });
            }
            foreach (var card in player2Cards)
            {
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    UserId = player2Id,
                    CardId = card.Id,
                    IsInHand = true
                });
            }
            // Remove used cards from deck
            deck.RemoveRange(0, 4);
            await context.SaveChangesAsync();
            return session;
        }

        public async Task<GameStateDto> GetGameStateAsync(Guid sessionId, Guid playerId)
        {
            using var context = _contextFactory.CreateDbContext();
            var session = await context.GameSessions
                .Include(gs => gs.PlayerCards)
                    .ThenInclude(pc => pc.Card)
                .Include(gs => gs.Bets)
                .Include(gs => gs.PlayerActions)
                .Include(gs => gs.Player1)
                .Include(gs => gs.Player2)
                .FirstOrDefaultAsync(gs => gs.Id == sessionId);

            if (session == null)
                throw new Exception("Game session not found.");

            var player = await context.Users.FindAsync(playerId);
            var opponentId = (session.Player1Id == playerId) ? session.Player2Id : session.Player1Id;
            var opponent = await context.Users.FindAsync(opponentId);

            var pocketCards = session.PlayerCards
                .Where(pc => pc.UserId == playerId && pc.IsInHand)
                .Select(pc => new CardDto { Rank = pc.Card.Rank, Suit = pc.Card.Suit })
                .ToList();
            var communityCards = session.PlayerCards
                .Where(pc => !pc.IsInHand)
                .Select(pc => new CardDto { Rank = pc.Card.Rank, Suit = pc.Card.Suit })
                .ToList();

            // Compute available actions
            bool isPlayerTurn = (session.CurrentTurnPlayerId == playerId);
            var available = new List<string>();
            if (isPlayerTurn)
            {
                if (session.CurrentBet == 0)
                {
                    available.Add("Check");
                    available.Add("Raise");
                }
                else
                {
                    available.Add("Fold");
                    available.Add("Call");
                    available.Add("Raise");
                }
            }

            var dto = new GameStateDto
            {
                PocketCards = pocketCards,
                CommunityCards = communityCards,
                Pot = session.Pot,
                CurrentBet = session.CurrentBet,
                PlayerStack = player.Chips,
                OpponentStack = opponent.Chips,
                Phase = session.Phase,
                IsPlayerTurn = isPlayerTurn,
                AvailableActions = available,
                BestCombination = null,
                BestWinningAmount = 0
            };

            // If showdown or completed, determine best combos and winning
            if (session.Phase == GamePhase.Showdown || session.Phase == GamePhase.Completed)
            {
                // Evaluate best combination for each player
                var community = session.PlayerCards.Where(pc => !pc.IsInHand).Select(pc => pc.Card).ToList();
                var playerCardsList = session.PlayerCards
                    .Where(pc => pc.UserId == playerId)
                    .Select(pc => pc.Card).ToList();
                var oppCardsList = session.PlayerCards
                    .Where(pc => pc.UserId == opponentId)
                    .Select(pc => pc.Card).ToList();

                var playerBest = EvaluateBestHand(playerCardsList.Concat(community).ToList());
                var oppBest = EvaluateBestHand(oppCardsList.Concat(community).ToList());

                // Determine winner
                bool playerWon = IsFirstHandBetter(playerBest, oppBest);
                string playerCombination = playerBest.Combination;
                decimal bestWinAmt = 0;
                if (playerWon)
                {
                    dto.BestCombination = playerCombination;
                    bestWinAmt = session.Pot; // winner takes pot
                }
                else if (!playerWon && session.WinnerId == opponentId)
                {
                    // Opponent won
                    dto.BestCombination = playerCombination; // still set for UI, though lost
                    bestWinAmt = 0;
                }
                dto.BestWinningAmount = bestWinAmt;
            }
            return dto;
        }

        public async Task SendActionAsync(Guid sessionId, ActionDto actionDto)
        {
            using var context = _contextFactory.CreateDbContext();
            var session = await context.GameSessions
                .Include(gs => gs.PlayerActions.OrderBy(a => a.Timestamp))
                .FirstOrDefaultAsync(gs => gs.Id == sessionId);
            if (session == null || !session.IsActive)
                throw new Exception("Game session not active.");

            var playerId = actionDto.PlayerId;
            var opponentId = (session.Player1Id == playerId) ? session.Player2Id : session.Player1Id;

            // Ensure turn
            if (session.CurrentTurnPlayerId != playerId)
                throw new Exception("Not this player's turn.");

            if (actionDto.Action == PokerData.Models.ActionType.Fold)
            {
                // Player folds, opponent wins
                await context.PlayerActions.AddAsync(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    ActionType = ActionType.Fold,
                    Timestamp = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
                // Set winner and end game
                session.WinnerId = opponentId;
                session.WinningCombination = null; // Will be set in DetermineWinner
                session.Phase = GamePhase.Completed;
                session.IsActive = false;
                await DetermineWinnerAsync(session, context);
                await context.SaveChangesAsync();
                return;
            }
            else if (actionDto.Action == PokerData.Models.ActionType.Raise)
            {
                decimal amount = actionDto.Amount ?? 0;
                if (amount <= 0) throw new Exception("Invalid raise amount.");
                var bet = new Bet
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    Amount = amount,
                    Timestamp = DateTime.UtcNow
                };
                await context.Bets.AddAsync(bet);
                await context.PlayerActions.AddAsync(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    ActionType = ActionType.Raise,
                    Bet = bet,
                    Timestamp = DateTime.UtcNow
                });
                session.Pot += amount;
                session.CurrentBet = amount;
                // Deduct chips from player
                var user = await context.Users.FindAsync(playerId);
                user.Chips -= amount;
                // Switch turn
                session.CurrentTurnPlayerId = opponentId;
                session.LastActionTime = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
            else if (actionDto.Action == PokerData.Models.ActionType.Call)
            {
                // Player calls current bet
                decimal callAmt = session.CurrentBet;
                var bet = new Bet
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    Amount = callAmt,
                    Timestamp = DateTime.UtcNow
                };
                await context.Bets.AddAsync(bet);
                await context.PlayerActions.AddAsync(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    ActionType = ActionType.Call,
                    Bet = bet,
                    Timestamp = DateTime.UtcNow
                });
                session.Pot += callAmt;
                // Deduct chips
                var user = await context.Users.FindAsync(playerId);
                user.Chips -= callAmt;
                session.CurrentBet = 0;
                session.LastActionTime = DateTime.UtcNow;
                // If final round (River), go to showdown; else advance phase
                if (session.Phase == GamePhase.River)
                {
                    session.Phase = GamePhase.Showdown;
                    await context.SaveChangesAsync();
                    await DetermineWinnerAsync(session, context);
                    session.IsActive = false;
                    await context.SaveChangesAsync();
                }
                else
                {
                    session.CurrentTurnPlayerId = null; // Will be set in AdvancePhase
                    await context.SaveChangesAsync();
                    await AdvancePhaseAsync(session, context);
                    await context.SaveChangesAsync();
                }
            }
            else if (actionDto.Action == PokerData.Models.ActionType.Check)
            {
                // If current bet is 0, check is allowed
                if (session.CurrentBet != 0)
                    throw new Exception("Cannot check when there is a bet.");
                await context.PlayerActions.AddAsync(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = playerId,
                    ActionType = ActionType.Check,
                    Timestamp = DateTime.UtcNow
                });
                session.LastActionTime = DateTime.UtcNow;
                var lastAction = session.PlayerActions.OrderByDescending(a => a.Timestamp).Skip(1).FirstOrDefault();
                bool isSecondCheck = lastAction != null
                    && lastAction.UserId != playerId
                    && lastAction.ActionType == ActionType.Check;
                if (isSecondCheck)
                {
                    // Both players checked, advance phase
                    if (session.Phase == GamePhase.River)
                    {
                        session.Phase = GamePhase.Showdown;
                        await context.SaveChangesAsync();
                        await DetermineWinnerAsync(session, context);
                        session.IsActive = false;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        await context.SaveChangesAsync();
                        await AdvancePhaseAsync(session, context);
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    // Switch turn to other player
                    session.CurrentTurnPlayerId = opponentId;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task AdvancePhaseAsync(GameSession session, MyDbContext context)
        {
            // Reveal community cards depending on phase
            var deck = await context.Cards.ToListAsync();
            // Remove used cards
            var usedIds = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id)
                .Select(pc => pc.CardId).ToListAsync();
            deck = deck.Where(c => !usedIds.Contains(c.Id)).OrderBy(c => Guid.NewGuid()).ToList();

            if (session.Phase == GamePhase.PreFlop)
            {
                // Reveal flop (3 cards)
                for (int i = 0; i < 3; i++)
                {
                    await context.PlayerCards.AddAsync(new PlayerCard
                    {
                        GameSessionId = session.Id,
                        CardId = deck[i].Id,
                        IsInHand = false
                    });
                }
                session.Phase = GamePhase.Flop;
            }
            else if (session.Phase == GamePhase.Flop)
            {
                // Reveal turn (1 card)
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    CardId = deck[3].Id,
                    IsInHand = false
                });
                session.Phase = GamePhase.Turn;
            }
            else if (session.Phase == GamePhase.Turn)
            {
                // Reveal river (1 card)
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    CardId = deck[4].Id,
                    IsInHand = false
                });
                session.Phase = GamePhase.River;
            }

            // Determine next turn: alternate who starts
            var lastAction = await context.PlayerActions
                .Where(a => a.GameSessionId == session.Id)
                .OrderByDescending(a => a.Timestamp).FirstOrDefaultAsync();
            if (lastAction != null)
            {
                session.CurrentTurnPlayerId = (lastAction.UserId == session.Player1Id) ? session.Player2Id : session.Player1Id;
            }
            session.CurrentBet = 0;
            session.LastActionTime = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }

        public async Task DetermineWinnerAsync(GameSession session, MyDbContext context)
        {
            // Get cards
            var community = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && !pc.IsInHand)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card).ToListAsync();
            var player1Cards = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && pc.UserId == session.Player1Id)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card).ToListAsync();
            var player2Cards = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && pc.UserId == session.Player2Id)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card).ToListAsync();

            var best1 = EvaluateBestHand(player1Cards.Concat(community).ToList());
            var best2 = EvaluateBestHand(player2Cards.Concat(community).ToList());

            bool player1Wins = IsFirstHandBetter(best1, best2);
            if (player1Wins)
            {
                session.WinnerId = session.Player1Id;
                session.WinningCombination = best1.Combination;
            }
            else
            {
                session.WinnerId = session.Player2Id;
                session.WinningCombination = best2.Combination;
            }

            // Update user stats: chips and rating
            if (player1Wins)
            {
                var user1 = await context.Users.FindAsync(session.Player1Id);
                var user2 = await context.Users.FindAsync(session.Player2Id);
                user1.Chips += session.Pot;
                user1.Rating += 1;
            }
            else
            {
                var user2 = await context.Users.FindAsync(session.Player2Id);
                var user1 = await context.Users.FindAsync(session.Player1Id);
                user2.Chips += session.Pot;
                user2.Rating += 1;
            }
        }

        // Helper: Evaluate best hand from a list of cards (>=5)
        private (int Rank, List<int> Tiebreakers, string Combination) EvaluateBestHand(List<Card> cards)
        {
            // Combination rank: HighCard=0, Pair=1, TwoPair=2, Trips=3, Straight=4, Flush=5, FullHouse=6, Quads=7, StraightFlush=8, RoyalFlush=9
            int bestRank = 0;
            List<int> bestTiebreak = new List<int>();
            string bestCombo = "High Card";
            // Generate all 5-card combinations
            int n = cards.Count;
            var idx = new int[5];
            for (idx[0] = 0; idx[0] < n - 4; idx[0]++)
                for (idx[1] = idx[0] + 1; idx[1] < n - 3; idx[1]++)
                    for (idx[2] = idx[1] + 1; idx[2] < n - 2; idx[2]++)
                        for (idx[3] = idx[2] + 1; idx[3] < n - 1; idx[3]++)
                            for (idx[4] = idx[3] + 1; idx[4] < n; idx[4]++)
                            {
                                var hand = new List<Card> { cards[idx[0]], cards[idx[1]], cards[idx[2]], cards[idx[3]], cards[idx[4]] };
                                var (rank, tiebreakers, combo) = Evaluate5CardHand(hand);
                                if (rank > bestRank || (rank == bestRank && CompareTiebreakers(tiebreakers, bestTiebreak) > 0))
                                {
                                    bestRank = rank;
                                    bestTiebreak = tiebreakers;
                                    bestCombo = combo;
                                }
                            }
            return (bestRank, bestTiebreak, bestCombo);
        }

        // Compare tiebreaker lists, returns 1 if first > second, -1 if first < second, 0 if equal
        private int CompareTiebreakers(List<int> first, List<int> second)
        {
            for (int i = 0; i < Math.Min(first.Count, second.Count); i++)
            {
                if (first[i] > second[i]) return 1;
                if (first[i] < second[i]) return -1;
            }
            return 0;
        }

        // Evaluate exactly 5-card hand
        private (int Rank, List<int> Tiebreakers, string Combination) Evaluate5CardHand(List<Card> hand)
        {
            // Sort by rank descending
            hand = hand.OrderByDescending(c => c.Rank).ToList();
            bool isFlush = hand.All(c => c.Suit == hand[0].Suit);
            bool isStraight = true;
            for (int i = 0; i < 4; i++)
            {
                if (hand[i].Rank != hand[i + 1].Rank + 1)
                {
                    isStraight = false;
                    break;
                }
            }
            // Special case: Ace-low straight (5-4-3-2-A)
            bool wheel = false;
            if (!isStraight && hand[0].Rank == 14 &&
                hand[1].Rank == 5 && hand[2].Rank == 4 && hand[3].Rank == 3 && hand[4].Rank == 2)
            {
                isStraight = true;
                wheel = true;
            }

            var groups = hand.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key);
            var counts = groups.Select(g => g.Count()).ToList();
            var ranks = groups.Select(g => g.Key).ToList();

            if (isFlush && isStraight)
            {
                if (hand[0].Rank == 14 && !wheel)
                    return (9, new List<int> { 14 }, "Royal Flush");
                return (8, new List<int> { wheel ? 5 : hand[0].Rank }, "Straight Flush");
            }
            if (counts[0] == 4)
            {
                // Four of a Kind
                int fourRank = ranks[0];
                int kicker = hand.First(c => c.Rank != fourRank).Rank;
                return (7, new List<int> { fourRank, kicker }, "Four of a Kind");
            }
            if (counts[0] == 3 && counts[1] == 2)
            {
                // Full House
                return (6, new List<int> { ranks[0], ranks[1] }, "Full House");
            }
            if (isFlush)
            {
                return (5, hand.Select(c => c.Rank).ToList(), "Flush");
            }
            if (isStraight)
            {
                return (4, new List<int> { wheel ? 5 : hand[0].Rank }, "Straight");
            }
            if (counts[0] == 3)
            {
                int threeRank = ranks[0];
                var kickers = hand.Where(c => c.Rank != threeRank).Select(c => c.Rank).ToList();
                return (3, new List<int> { threeRank }.Concat(kickers).ToList(), "Three of a Kind");
            }
            if (counts[0] == 2 && counts[1] == 2)
            {
                // Two Pair
                int highPair = ranks[0];
                int lowPair = ranks[1];
                int kicker = hand.First(c => c.Rank != highPair && c.Rank != lowPair).Rank;
                return (2, new List<int> { highPair, lowPair, kicker }, "Two Pair");
            }
            if (counts[0] == 2)
            {
                // One Pair
                int pairRank = ranks[0];
                var kickers = hand.Where(c => c.Rank != pairRank).Select(c => c.Rank).ToList();
                return (1, new List<int> { pairRank }.Concat(kickers).ToList(), "One Pair");
            }
            // High card
            return (0, hand.Select(c => c.Rank).ToList(), "High Card");
        }

        // Return true if first hand is better than second
        private bool IsFirstHandBetter((int Rank, List<int> Tiebreakers, string Combination) first,
                                      (int Rank, List<int> Tiebreakers, string Combination) second)
        {
            if (first.Rank != second.Rank)
                return first.Rank > second.Rank;
            int comp = CompareTiebreakers(first.Tiebreakers, second.Tiebreakers);
            return comp > 0;
        }
    }
}
