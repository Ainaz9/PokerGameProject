using PokerGameRSF.DataAccess;
using PokerGameRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Services
{
    public static class GameSessionManager
    {
        private const int TurnTimeoutSeconds = 30;

        // Начать новую игровую сессию для двух игроков
        public static GameSession StartGame(Guid player1Id, Guid player2Id)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                // настройка сессии 
                var session = new GameSession
                {
                    Player1Id = player1Id,
                    Player2Id = player2Id,
                    Phase = GamePhase.PreFlop,
                    Pot = 0,
                    IsActive = true
                };
                // Рандомно кто начинает
                var rnd = new Random();
                session.CurrentTurnPlayerId = (rnd.Next(2) == 0) ? player1Id : player2Id;
                session.LastActionTime = DateTime.UtcNow;
                context.GameSessions.Add(session);
                context.SaveChanges();

                // Раздача карт из колоды (тасовка)
                var deck = context.Cards.ToList();
                var rand = new Random();
                deck = deck.OrderBy(c => rand.Next()).ToList();

                // 2 карты игрокам (каждому)
                var player1Cards = deck.Take(2).ToList();
                var player2Cards = deck.Skip(2).Take(2).ToList();
                foreach (var card in player1Cards)
                {
                    context.PlayerCards.Add(new PlayerCard
                    {
                        GameSessionId = session.Id,
                        UserId = player1Id,
                        CardId = card.Id
                    });
                }
                foreach (var card in player2Cards)
                {
                    context.PlayerCards.Add(new PlayerCard
                    {
                        GameSessionId = session.Id,
                        UserId = player2Id,
                        CardId = card.Id
                    });
                }
                // удалить использованные карм карты
                deck.RemoveRange(0, 4);

                // Распределить карты на столе (флоп + терн + ривер)
                // Позиции 1-3: флоп, 4: терн, 5: ривер
                for (int i = 0; i < 5; i++)
                {
                    context.BoardCards.Add(new BoardCard
                    {
                        GameSessionId = session.Id,
                        CardId = deck[i].Id,
                        Position = i + 1
                    });
                }
                context.SaveChanges();
                return session;
            }
        }

        // Получить активную игровую сессию для пользователя
        public static GameSession GetActiveSessionForUser(Guid userId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                return context.GameSessions
                    .Where(gs => gs.IsActive && (gs.Player1Id == userId || gs.Player2Id == userId))
                    .FirstOrDefault();
            }
        }

        // Обработка действий игрока, совершающих сброс карт
        public static void PlayerFold(Guid userId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.IsActive && gs.CurrentTurnPlayerId == userId);
                if (session == null) return;
                // запись Fold
                context.PlayerActions.Add(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = userId,
                    ActionType = ActionType.Fold,
                    Timestamp = DateTime.UtcNow
                });
                // Соперник выигрывает банк!
                var opponentId = (session.Player1Id == userId) ? session.Player2Id : session.Player1Id;
                var opponent = context.Users.Find(opponentId);
                opponent.Chips += session.Pot;
                opponent.Rating += 1;
                var loser = context.Users.Find(userId);
                loser.Rating -= 1;

                // Конец игровой сессии
                session.IsActive = false;
                session.Phase = GamePhase.Completed;
                context.SaveChanges();
            }
        }

        // Обработка проверки действий игрока
        public static void PlayerCheck(Guid userId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.IsActive && gs.CurrentTurnPlayerId == userId);
                if (session == null) return;

                // записываем Check
                context.PlayerActions.Add(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = userId,
                    ActionType = ActionType.Check,
                    Timestamp = DateTime.UtcNow
                });
                session.LastActionTime = DateTime.UtcNow;
                context.SaveChanges();

                // Определить противника
                var opponentId = (session.Player1Id == userId) ? session.Player2Id : session.Player1Id;
                // Проверка сделали ли оба чек
                var lastTwo = context.PlayerActions
                    .Where(pa => pa.GameSessionId == session.Id)
                    .OrderByDescending(pa => pa.Timestamp)
                    .Take(2)
                    .ToList();
                if (lastTwo.Count == 2
                    && lastTwo[0].ActionType == ActionType.Check
                    && lastTwo[1].ActionType == ActionType.Check)
                {
                    AdvanceGamePhase(session.Id);
                }
                else
                {
                    // Переключаем ход (противнику)
                    session.CurrentTurnPlayerId = opponentId;
                    session.LastActionTime = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
        }

        // Обработка call
        public static void PlayerCall(Guid userId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.IsActive && gs.CurrentTurnPlayerId == userId);
                if (session == null) return;

                var opponentId = (session.Player1Id == userId) ? session.Player2Id : session.Player1Id;
                // Найдите последнее действие противника
                var lastAction = context.PlayerActions
                    .Where(pa => pa.GameSessionId == session.Id && pa.UserId != userId)
                    .OrderByDescending(pa => pa.Timestamp)
                    .FirstOrDefault();
                // Если оппонент повысил ставку, уравниваем
                if (lastAction != null && lastAction.ActionType == ActionType.Raise)
                {
                    var raiseBet = context.Bets.Find(lastAction.BetId);
                    if (raiseBet != null)
                    {
                        int amount = raiseBet.Amount;
                        var user = context.Users.Find(userId);
                        if (user.Chips >= amount)
                        {
                            // Вычесть фишки, обновить банк
                            user.Chips -= amount;
                            session.Pot += amount;
                            // ставка
                            var bet = new Bet
                            {
                                GameSessionId = session.Id,
                                UserId = userId,
                                Amount = amount,
                                Timestamp = DateTime.UtcNow
                            };
                            context.Bets.Add(bet);
                            context.SaveChanges();
                            // Запись call
                            context.PlayerActions.Add(new PlayerAction
                            {
                                GameSessionId = session.Id,
                                UserId = userId,
                                ActionType = ActionType.Call,
                                BetId = bet.Id,
                                Timestamp = DateTime.UtcNow
                            });
                            session.LastActionTime = DateTime.UtcNow;
                            context.SaveChanges();
                        }
                        else
                        {
                            // Недостаточно фишек: считать фолдом гыгы
                            PlayerFold(userId);
                            return;
                        }
                    }
                    // После вызова, предварительная фаза
                    AdvanceGamePhase(session.Id);
                }
                else
                {
                    // Нет соответствующей ставки: считать как чек
                    PlayerCheck(userId);
                }
            }
        }

        // Обработываем действие игрока по повышению ставки
        public static void PlayerRaise(Guid userId, int raiseAmount)
        {
            if (raiseAmount <= 0) return;
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.IsActive && gs.CurrentTurnPlayerId == userId);
                if (session == null) return;
                var user = context.Users.Find(userId);
                if (user.Chips < raiseAmount) return;
                var opponentId = (session.Player1Id == userId) ? session.Player2Id : session.Player1Id;

                // Вычесть фишки и обновить банк
                user.Chips -= raiseAmount;
                session.Pot += raiseAmount;
                // Записываем ставку
                var bet = new Bet
                {
                    GameSessionId = session.Id,
                    UserId = userId,
                    Amount = raiseAmount,
                    Timestamp = DateTime.UtcNow
                };
                context.Bets.Add(bet);
                context.SaveChanges();
                // Записываем raise action
                context.PlayerActions.Add(new PlayerAction
                {
                    GameSessionId = session.Id,
                    UserId = userId,
                    ActionType = ActionType.Raise,
                    BetId = bet.Id,
                    Timestamp = DateTime.UtcNow
                });
                // Переключить ход на противника
                session.CurrentTurnPlayerId = opponentId;
                session.LastActionTime = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        // Концовка фаза игры (флоп -> тёрн -> ривер -> вскрытие карт)
        private static void AdvanceGamePhase(Guid sessionId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.Id == sessionId && gs.IsActive);
                if (session == null) return;

                if (session.Phase == GamePhase.PreFlop)
                {
                    session.Phase = GamePhase.Flop;
                }
                else if (session.Phase == GamePhase.Flop)
                {
                    session.Phase = GamePhase.Turn;
                }
                else if (session.Phase == GamePhase.Turn)
                {
                    session.Phase = GamePhase.River;
                }
                else if (session.Phase == GamePhase.River)
                {
                    session.Phase = GamePhase.Showdown;
                    // определить победителя на вскрытии
                    DetermineWinner(session.Id);
                }
                // Следующий этап: начнинаем с Player1
                session.CurrentTurnPlayerId = session.Player1Id;
                session.LastActionTime = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        // Объявление победителя игры
        private static void DetermineWinner(Guid sessionId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.Id == sessionId && gs.IsActive);
                if (session == null) return;

                // Собериаем карты каждого игрока.
                var player1Cards = context.PlayerCards
                    .Where(pc => pc.GameSessionId == sessionId && pc.UserId == session.Player1Id)
                    .Select(pc => pc.Card).ToList();
                var player2Cards = context.PlayerCards
                    .Where(pc => pc.GameSessionId == sessionId && pc.UserId == session.Player2Id)
                    .Select(pc => pc.Card).ToList();
                // Общие карты (5 карт)
                var communityCards = context.BoardCards
                    .Where(bc => bc.GameSessionId == sessionId)
                    .OrderBy(bc => bc.Position)
                    .Select(bc => bc.Card).ToList();

                // Комбинируем карманные и общие карты (по 7 карт у каждого)
                var hand1 = new List<Card>(player1Cards);
                hand1.AddRange(communityCards);
                var hand2 = new List<Card>(player2Cards);
                hand2.AddRange(communityCards);

                // Оцениваем лучшую комбинацию из 5 карт для каждого игрока
                var rank1 = EvaluateBestHand(hand1);
                var rank2 = EvaluateBestHand(hand2);

                Guid? winnerId = null;
                bool tie = false;
                if (rank1.Item1 > rank2.Item1)
                    winnerId = session.Player1Id;
                else if (rank2.Item1 > rank1.Item1)
                    winnerId = session.Player2Id;
                else
                {
                    // Тай-брейк с рейтинговыми списками
                    for (int i = 0; i < rank1.Item2.Count; i++)
                    {
                        if (i >= rank2.Item2.Count) break;
                        if (rank1.Item2[i] > rank2.Item2[i])
                        {
                            winnerId = session.Player1Id;
                            break;
                        }
                        else if (rank2.Item2[i] > rank1.Item2[i])
                        {
                            winnerId = session.Player2Id;
                            break;
                        }
                    }
                    if (winnerId == null) tie = true;
                }

                // Распределяем банк и обновляем рейтинги
                if (tie)
                {
                    // Разделяем банк
                    var half = session.Pot / 2;
                    var u1 = context.Users.Find(session.Player1Id);
                    var u2 = context.Users.Find(session.Player2Id);
                    u1.Chips += half;
                    u2.Chips += (session.Pot - half);
                }
                else if (winnerId == session.Player1Id)
                {
                    var winner = context.Users.Find(session.Player1Id);
                    winner.Chips += session.Pot;
                    winner.Rating += 1;
                    var loser = context.Users.Find(session.Player2Id);
                    loser.Rating -= 1;
                }
                else if (winnerId == session.Player2Id)
                {
                    var winner = context.Users.Find(session.Player2Id);
                    winner.Chips += session.Pot;
                    winner.Rating += 1;
                    var loser = context.Users.Find(session.Player1Id);
                    loser.Rating -= 1;
                }

                // Конец игровой сессии
                session.IsActive = false;
                session.Phase = GamePhase.Completed;
                context.SaveChanges();
            }
        }

        // Оцениваем лучшую комбинацию из 5 карт из 7 карт
        private static Tuple<int, List<int>> EvaluateBestHand(List<Card> cards)
        {
            var bestRank = Tuple.Create(0, new List<int>());
            int n = cards.Count;
            var indices = new int[n];
            for (int i = 0; i < n; i++) indices[i] = i;

            // Генерируем все комбинации из 5 карт
            foreach (var combo in GetCombinations(indices, 5))
            {
                var hand = combo.Select(i => cards[i]).ToList();
                var rank = EvaluateHand(hand);
                if (rank.Item1 > bestRank.Item1 ||
                    (rank.Item1 == bestRank.Item1 && CompareRankLists(rank.Item2, bestRank.Item2) > 0))
                {
                    bestRank = rank;
                }
            }
            return bestRank;
        }

        // Сравните два Rank карты из списка 
        private static int CompareRankLists(List<int> a, List<int> b)
        {
            for (int i = 0; i < Math.Min(a.Count, b.Count); i++)
            {
                if (a[i] != b[i]) return a[i].CompareTo(b[i]);
            }
            return 0;
        }

        // Помощник для получения комбинаций из каких-то k элементов
        private static List<List<int>> GetCombinations(int[] indices, int k)
        {
            var result = new List<List<int>>();
            GetCombRecursive(indices, k, 0, new List<int>(), result);
            return result;
        }
        private static void GetCombRecursive(int[] indices, int k, int start, List<int> current, List<List<int>> result)
        {
            if (current.Count == k)
            {
                result.Add(new List<int>(current));
                return;
            }
            for (int i = start; i < indices.Length; i++)
            {
                current.Add(indices[i]);
                GetCombRecursive(indices, k, i + 1, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }

        // Оцениваем карты игрока (5 карт): возврат (категория, ранк, список для тай-брейка)
        // Категория: 8=Стрит-Флеш,7=Четыре,6=Фуллхаус,5=Флеш,4=Стрит,3=Три,2=Две пары,1=Одна пара,0=Старшая карта
        private static Tuple<int, List<int>> EvaluateHand(List<Card> hand)
        {
            // Сортируем по убыванию
            var ranks = hand.Select(c => c.Rank).OrderByDescending(r => r).ToList();
            var suits = hand.Select(c => c.Suit).ToList();

            bool flush = suits.GroupBy(s => s).Any(g => g.Count() == 5);
            bool straight = IsStraight(ranks);
            var rankGroups = ranks.GroupBy(r => r)
                                  .OrderByDescending(g => g.Count())
                                  .ThenByDescending(g => g.Key)
                                  .ToList();

            // Straight Flush
            if (flush && straight)
            {
                return Tuple.Create(8, new List<int> { ranks.Max() });
            }
            // Four of a kind
            if (rankGroups[0].Count() == 4)
            {
                int fourRank = rankGroups[0].Key;
                int kicker = rankGroups[1].Key;
                return Tuple.Create(7, new List<int> { fourRank, kicker });
            }
            // Full House (3 + 2)
            if (rankGroups[0].Count() == 3 && rankGroups.Count > 1 && rankGroups[1].Count() >= 2)
            {
                int threeRank = rankGroups[0].Key;
                int pairRank = rankGroups[1].Key;
                return Tuple.Create(6, new List<int> { threeRank, pairRank });
            }
            // Flush
            if (flush)
            {
                return Tuple.Create(5, ranks);
            }
            // Straight
            if (straight)
            {
                return Tuple.Create(4, new List<int> { ranks.Max() });
            }
            // Three of a kind
            if (rankGroups[0].Count() == 3)
            {
                int threeRank = rankGroups[0].Key;
                var kickers = rankGroups.Skip(1).Select(g => g.Key).Take(2).ToList();
                return Tuple.Create(3, new List<int> { threeRank }.Concat(kickers).ToList());
            }
            // Two Pair
            if (rankGroups[0].Count() == 2 && rankGroups.Count > 1 && rankGroups[1].Count() == 2)
            {
                int highPair = rankGroups[0].Key;
                int lowPair = rankGroups[1].Key;
                int kicker = rankGroups.Skip(2).Select(g => g.Key).First();
                return Tuple.Create(2, new List<int> { highPair, lowPair, kicker });
            }
            // One Pair
            if (rankGroups[0].Count() == 2)
            {
                int pairRank = rankGroups[0].Key;
                var kickers = rankGroups.Skip(1).Select(g => g.Key).Take(3).ToList();
                return Tuple.Create(1, new List<int> { pairRank }.Concat(kickers).ToList());
            }
            // High Card
            return Tuple.Create(0, ranks);
        }

        // Check for straight in sorted ranks (handles Ace-low as well)
        private static bool IsStraight(List<int> ranks)
        {
            var distinct = ranks.Distinct().ToList();
            // Special case: A-2-3-4-5
            if (distinct.Contains(14))
            {
                var lowStraight = new[] { 5, 4, 3, 2, 14 };
                if (lowStraight.All(v => distinct.Contains(v))) return true;
            }
            // 
            for (int i = 0; i < distinct.Count - 4; i++)
            {
                if (distinct[i] - 1 == distinct[i+1] &&
                    distinct[i+1] - 1 == distinct[i+2] &&
                    distinct[i+2] - 1 == distinct[i+3] &&
                    distinct[i+3] - 1 == distinct[i+4])
                {
                    return true;
                }
            }
            return false;
        }

        // Тайм-аут: автоматическое fold при превышении таймаута
        public static void HandleTimeout(Guid userId)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions
                    .FirstOrDefault(gs => gs.IsActive && gs.CurrentTurnPlayerId == userId);
                if (session == null) return;
                var elapsed = (DateTime.UtcNow - session.LastActionTime).TotalSeconds;
                if (elapsed >= TurnTimeoutSeconds)
                {
                    PlayerFold(userId);
                }
            }
        }
    }
}
