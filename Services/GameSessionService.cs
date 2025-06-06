using Microsoft.EntityFrameworkCore;
using PokerGame.Models;
using PokerGameRSF.DTO;
using PokerGameRSF.Models;

namespace PokerGameRSF.Services
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
            // Включаем отслеживание изменений (чтобы приложение работало) без этого крах
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Формируем кролоду карт и пермешиваем 
            var deck = await context.Cards.ToListAsync();
            var random = new Random();
            deck = deck.OrderBy(_ => random.Next()).ToList();

            var session = new GameSession
            {
                Player1Id = player1Id,
                Player2Id = player2Id,
                Pot = 0,
                Phase = GamePhase.PreFlop,
                IsActive = true,
                CurrentTurnPlayerId = player1Id,
                CurrentBet = 0,
                WinningCombination = null
            };
            context.GameSessions.Add(session);
            await context.SaveChangesAsync();

            // Формируем колоды крат игроков
            var player1Cards = deck.Take(2).ToList();
            var player2Cards = deck.Skip(2).Take(2).ToList();

            foreach (var card in player1Cards)
            {
                context.PlayerCards.Add(new PlayerCard
                {
                    GameSessionId = session.Id,
                    UserId = player1Id,
                    CardId = card.Id,
                    IsInHand = true
                });
            }

            foreach (var card in player2Cards)
            {
                context.PlayerCards.Add(new PlayerCard
                {
                    GameSessionId = session.Id,
                    UserId = player2Id,
                    CardId = card.Id,
                    IsInHand = true
                });
            }

            // Убираем первые 4 карты из колоды (которые уже используются) чтобы не было повторов
            deck.RemoveRange(0, 4);
            await context.SaveChangesAsync();

            return session;
        }

        public async Task<GameStateDto> GetGameStateAsync(Guid sessionId, Guid playerId)
        {
            using var context = _contextFactory.CreateDbContext();
            var session = await context.GameSessions
                .Include(gs => gs.PlayerCards).ThenInclude(pc => pc.Card)
                .Include(gs => gs.Bets)
                .Include(gs => gs.PlayerActions)
                .Include(gs => gs.Player1)
                .Include(gs => gs.Player2)
                .FirstOrDefaultAsync(gs => gs.Id == sessionId);

            if (session == null)
                throw new Exception("Game session не найдена.");

            var player = await context.Users.FindAsync(playerId);
            var opponentId = session.Player1Id == playerId ? session.Player2Id : session.Player1Id;
            var opponent = await context.Users.FindAsync(opponentId);

            var pocketCards = session.PlayerCards
                .Where(pc => pc.UserId == playerId && pc.IsInHand)
                .Select(pc => new CardDto { Rank = pc.Card.Rank, Suit = pc.Card.Suit })
                .ToList();

            var communityCards = session.PlayerCards
                .Where(pc => !pc.IsInHand)
                .Select(pc => new CardDto { Rank = pc.Card.Rank, Suit = pc.Card.Suit })
                .ToList();

            bool isPlayerTurn = session.CurrentTurnPlayerId == playerId;
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
            
            if (session.Phase == GamePhase.Showdown || session.Phase == GamePhase.Completed)
            {
                var community = session.PlayerCards
                    .Where(pc => !pc.IsInHand)
                    .Select(pc => pc.Card)
                    .ToList();

                var playerCardsList = session.PlayerCards
                    .Where(pc => pc.UserId == playerId)
                    .Select(pc => pc.Card)
                    .ToList();

                var oppCardsList = session.PlayerCards
                    .Where(pc => pc.UserId == opponentId)
                    .Select(pc => pc.Card)
                    .ToList();

                var playerBest = EvaluateBestHand(playerCardsList.Concat(community).ToList());
                var oppBest = EvaluateBestHand(oppCardsList.Concat(community).ToList());

                bool playerWon = IsFirstHandBetter(playerBest, oppBest);
                if (playerWon)
                {
                    dto.BestCombination = playerBest.Combination;
                    dto.BestWinningAmount = session.Pot;
                }
                else if (!playerWon && session.WinnerId == opponentId)
                {
                    dto.BestCombination = playerBest.Combination;
                    dto.BestWinningAmount = 0;
                }
            }

            return dto;
        }

        public async Task SendActionAsync(Guid sessionId, ActionDto actionDto)
        {
            using var context = _contextFactory.CreateDbContext();
            // Включаем трекинг, иначе изменения не сохранятся И Крах
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            
            var session = await context.GameSessions
                .Include(gs => gs.PlayerActions)
                .Include(gs => gs.PlayerCards)
                .FirstOrDefaultAsync(gs => gs.Id == sessionId);

            if (session == null || !session.IsActive)
                throw new Exception("Сессия неактивна.");

            if (session.CurrentTurnPlayerId != actionDto.PlayerId)
                throw new Exception("Не ваша очередь.");

            var opponentId = session.Player1Id == actionDto.PlayerId
                ? session.Player2Id
                : session.Player1Id;

            switch (actionDto.Action)
            {
                case ActionType.Fold:
                    await context.PlayerActions.AddAsync(new PlayerAction
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        ActionType = ActionType.Fold
                    });

                    session.WinnerId = opponentId;
                    session.Phase = GamePhase.Completed;
                    session.IsActive = false;

                    await DetermineWinnerAsync(session, context);
                    break;

                case ActionType.Raise:
                    var raiseAmount = actionDto.Amount ?? 0;
                    if (raiseAmount <= 0)
                        throw new Exception("Неверная сумма Raise.");

                    var raisingUser = await context.Users.FindAsync(actionDto.PlayerId);
                    if (raisingUser.Chips < raiseAmount)
                        throw new Exception("Недостаточно фишек.");

                    raisingUser.Chips -= raiseAmount;
                    session.Pot += raiseAmount;
                    session.CurrentBet = raiseAmount;

                    await context.Bets.AddAsync(new Bet
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        Amount = raiseAmount
                    });

                    await context.PlayerActions.AddAsync(new PlayerAction
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        ActionType = ActionType.Raise
                    });

                    session.CurrentTurnPlayerId = opponentId;
                    break;

                case ActionType.Call:
                    decimal callAmount = session.CurrentBet;
                    if (callAmount == 0)
                        throw new Exception("Нет текущей ставки для Call.");

                    var callingUser = await context.Users.FindAsync(actionDto.PlayerId);
                    if (callingUser.Chips < callAmount)
                        throw new Exception("Недостаточно фишек для Call.");

                    callingUser.Chips -= callAmount;
                    session.Pot += callAmount;
                    session.CurrentBet = 0;

                    await context.Bets.AddAsync(new Bet
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        Amount = callAmount
                    });

                    await context.PlayerActions.AddAsync(new PlayerAction
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        ActionType = ActionType.Call
                    });

                    // Если сейчас River сразу в Showdown
                    if (session.Phase == GamePhase.River)
                    {
                        await DetermineWinnerAsync(session, context);
                        session.Phase = GamePhase.Completed;
                        session.IsActive = false;
                    }
                    else
                    {
                        // иначе след фаза игры
                        await AdvancePhaseAsync(session, context);
                    }
                    break;

                case ActionType.Check:
                    if (session.CurrentBet != 0)
                        throw new Exception("Нельзя чекнуть при активной ставке.");

                    await context.PlayerActions.AddAsync(new PlayerAction
                    {
                        GameSessionId = sessionId,
                        UserId = actionDto.PlayerId,
                        ActionType = ActionType.Check
                    });

                    var lastActions = await context.PlayerActions
                        .Where(a => a.GameSessionId == sessionId)
                        .OrderByDescending(a => a.Timestamp)
                        .Take(2)
                        .ToListAsync();

                    bool bothChecked = lastActions.Count == 2
                                       && lastActions.All(a => a.ActionType == ActionType.Check)
                                       && lastActions.Select(a => a.UserId).Distinct().Count() == 2;

                    if (bothChecked)
                    {
                        if (session.Phase == GamePhase.River)
                        {
                            await DetermineWinnerAsync(session, context);
                            session.Phase = GamePhase.Completed;
                            session.IsActive = false;
                        }
                        else
                        {
                            await AdvancePhaseAsync(session, context);
                        }
                    }
                    else
                    {
                        session.CurrentTurnPlayerId = opponentId;
                    }
                    break;

                default:
                    throw new Exception("Неизвестное действие.");
            }

            // Единре сохранение всех изменений
            await context.SaveChangesAsync();
        }

        public async Task AdvancePhaseAsync(GameSession session, MyDbContext context)
        {
            var deck = await context.Cards.ToListAsync();
            var usedCardIds = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id)
                .Select(pc => pc.CardId)
                .ToListAsync();

            deck = deck.Where(c => !usedCardIds.Contains(c.Id))
                       .OrderBy(_ => Guid.NewGuid())
                       .ToList();

            if (session.Phase == GamePhase.PreFlop && deck.Count < 3)
                throw new Exception("Недостаточно карт для флопа.");
            if (session.Phase == GamePhase.Flop && deck.Count < 1)
                throw new Exception("Недостаточно карт для терна.");
            if (session.Phase == GamePhase.Turn && deck.Count < 1)
                throw new Exception("Недостаточно карт для ривера.");

            if (session.Phase == GamePhase.PreFlop)
            {
                // Флоп открывает 3 карты на стол
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
                // Терн открытие 1 карты
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    CardId = deck[0].Id,
                    IsInHand = false
                });
                session.Phase = GamePhase.Turn;
            }
            else if (session.Phase == GamePhase.Turn)
            {
                // Ривер также 1 карта открытие
                await context.PlayerCards.AddAsync(new PlayerCard
                {
                    GameSessionId = session.Id,
                    CardId = deck[0].Id,
                    IsInHand = false
                });
                session.Phase = GamePhase.River;
            }

            // После сдачи карт ход возвращаем к Player
            session.CurrentTurnPlayerId = session.Player1Id;
        }

        public async Task DetermineWinnerAsync(GameSession session, MyDbContext context)
        {
            var community = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && !pc.IsInHand)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card)
                .ToListAsync();

            var player1Cards = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && pc.UserId == session.Player1Id)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card)
                .ToListAsync();

            var player2Cards = await context.PlayerCards
                .Where(pc => pc.GameSessionId == session.Id && pc.UserId == session.Player2Id)
                .Include(pc => pc.Card)
                .Select(pc => pc.Card)
                .ToListAsync();

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

            if (player1Wins)
            {
                var user1 = await context.Users.FindAsync(session.Player1Id);
                user1.Chips += session.Pot;
                user1.Rating += 5;
            }
            else
            {
                var user2 = await context.Users.FindAsync(session.Player2Id);
                user2.Chips += session.Pot;
                user2.Rating += 5;
            }

            await context.SaveChangesAsync();
        }

        // Оцениваем лучшу. комбинацию
        private (int Rank, List<int> Tiebreakers, string Combination) EvaluateBestHand(List<Card> cards)
        {
            int bestRank = 0;
            List<int> bestTiebreak = new List<int>();
            string bestCombo = "High Card";

            int n = cards.Count;
            var idx = new int[5];
            for (idx[0] = 0; idx[0] < n - 4; idx[0]++)
                for (idx[1] = idx[0] + 1; idx[1] < n - 3; idx[1]++)
                    for (idx[2] = idx[1] + 1; idx[2] < n - 2; idx[2]++)
                        for (idx[3] = idx[2] + 1; idx[3] < n - 1; idx[3]++)
                            for (idx[4] = idx[3] + 1; idx[4] < n; idx[4]++)
                            {
                                var hand = new List<Card>
                                {
                                    cards[idx[0]],
                                    cards[idx[1]],
                                    cards[idx[2]],
                                    cards[idx[3]],
                                    cards[idx[4]]
                                };
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

        private int CompareTiebreakers(List<int> first, List<int> second)
        {
            for (int i = 0; i < Math.Min(first.Count, second.Count); i++)
            {
                if (first[i] > second[i]) return 1;
                if (first[i] < second[i]) return -1;
            }
            return 0;
        }

        private (int Rank, List<int> Tiebreakers, string Combination) Evaluate5CardHand(List<Card> hand)
        {
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

            bool wheel = false;
            if (!isStraight && hand[0].Rank == 14 &&
                hand[1].Rank == 5 && hand[2].Rank == 4 && hand[3].Rank == 3 && hand[4].Rank == 2)
            {
                isStraight = true;
                wheel = true;
            }

            var groups = hand.GroupBy(c => c.Rank)
                             .OrderByDescending(g => g.Count())
                             .ThenByDescending(g => g.Key);
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
                int fourRank = ranks[0];
                int kicker = hand.First(c => c.Rank != fourRank).Rank;
                return (7, new List<int> { fourRank, kicker }, "Four of a Kind");
            }
            if (counts[0] == 3 && counts[1] == 2)
            {
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
                int highPair = ranks[0];
                int lowPair = ranks[1];
                int kicker = hand.First(c => c.Rank != highPair && c.Rank != lowPair).Rank;
                return (2, new List<int> { highPair, lowPair, kicker }, "Two Pair");
            }
            if (counts[0] == 2)
            {
                int pairRank = ranks[0];
                var kickers = hand.Where(c => c.Rank != pairRank).Select(c => c.Rank).ToList();
                return (1, new List<int> { pairRank }.Concat(kickers).ToList(), "One Pair");
            }
            return (0, hand.Select(c => c.Rank).ToList(), "High Card");
        }

        private bool IsFirstHandBetter(
            (int Rank, List<int> Tiebreakers, string Combination) first,
            (int Rank, List<int> Tiebreakers, string Combination) second)
        {
            if (first.Rank != second.Rank)
                return first.Rank > second.Rank;
            int comp = CompareTiebreakers(first.Tiebreakers, second.Tiebreakers);
            return comp > 0;
        }
    }
}
