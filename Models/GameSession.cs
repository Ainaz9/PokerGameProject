namespace PokerGamesRSF.Models
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
        public Guid? CurrentTurnPlayerId { get; set; }
        public virtual User CurrentTurnPlayer { get; set; }
        /// <summary>
        /// Фаза игры
        /// </summary>
        public GamePhase Phase { get; set; } = GamePhase.PreFlop;
        public decimal Pot { get; set; } = 0;
        public decimal CurrentBet { get; set; } = 0;
        public DateTime LastActionTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Guid? WinnerId { get; set; }
        /// <summary>
        /// Навигационный объект победителя
        /// </summary>
        public virtual User Winner { get; set; }
        public string WinningCombination { get; set; }

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
