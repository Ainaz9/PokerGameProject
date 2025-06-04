namespace PokerGamesRSF.Models
{
    /// <summary>
    /// Действия игрока
    /// </summary>
    public class PlayerAction
    {
        /// <summary>
        /// Уникальный идентификатор действия 
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Уникальный идентификатор игровой сессии
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Навигационный обхект игровой сессии 
        /// </summary>
        public virtual GameSession GameSession { get; set; }
        /// <summary>
        /// Уникальный идентификатор игрока совершающий действие 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Навигационный объект игрока, который выполнил действие 
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Действие игрока
        /// </summary>
        public ActionType ActionType { get; set; }
        /// <summary>
        /// Дата и время выполнения действия 
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Уникальный идентификатор ставки 
        /// </summary>
        public Guid? BetId { get; set; }
        /// <summary>
        /// Навигационный объект ставки
        /// </summary>
        public virtual Bet Bet { get; set; }
    }
}
