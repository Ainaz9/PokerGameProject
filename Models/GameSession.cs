using PokerGameRSF.Models;

namespace PokerGame.Models
{
    /// <summary>
    /// Игровая сессия описывает игру
    /// </summary>
    public class GameSession
    {
        /// <summary>
        /// Уникальный идентификатор игровой сессии
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Идентификатор первого игрока
        /// </summary>
        public Guid Player1Id { get; set; }
        /// <summary>
        /// Навигационный объект первого игрока
        /// </summary>
        public virtual User Player1 { get; set; }
        /// <summary>
        /// Идентификатор второго игрока
        /// </summary>
        public Guid Player2Id { get; set; }
        /// <summary>
        /// Навигационный объект второго игрока
        /// </summary>
        public virtual User Player2 { get; set; }
        /// <summary>
        /// Текущий ход игрока (кто)
        /// </summary>
        public Guid? CurrentTurnPlayerId { get; set; }
        /// <summary>
        /// Навигационное свойство (кто ходит)
        /// </summary>
        public virtual User CurrentTurnPlayer { get; set; }
        /// <summary>
        /// Фаза игры
        /// </summary>
        public GamePhase Phase { get; set; } = GamePhase.PreFlop;
        /// <summary>
        /// Банк
        /// </summary>
        public decimal Pot { get; set; } = 0;
        /// <summary>
        /// Текущая ставка
        /// </summary>
        public decimal CurrentBet { get; set; } = 0;
        /// <summary>
        /// Последнее действие (дата и время)
        /// </summary>
        public DateTime LastActionTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Активность игровой сессии
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// Победитель
        /// </summary>
        public Guid? WinnerId { get; set; }
        /// <summary>
        /// Навигационный объект победителя
        /// </summary>
        public virtual User Winner { get; set; }
        /// <summary>
        /// Выиграшная комбинация
        /// </summary>
        public string WinningCombination { get; set; }
        /// <summary>
        /// Связь с PlayerCards
        /// </summary>
        public virtual ICollection<PlayerCard> PlayerCards { get; set; } = new List<PlayerCard>();
        /// <summary>
        /// Связь с PlayerAction
        /// </summary>
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        /// <summary>
        /// Связь с Bet
        /// </summary>
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
