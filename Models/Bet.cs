namespace PokerGamesRSF.Models
{
    /// <summary>
    /// Ставка пользователя, сделанная в период игровой сессии
    /// </summary>
    public class Bet
    {
        /// <summary>
        /// Уникальный идентификатор ставки
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Уникальный идентификатор игровой сессии, к которой относится ставка
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Навигацонное свойство игровой сессии
        /// </summary>
        public virtual GameSession GameSession { get; set; }
        /// <summary>
        /// Уникальный идентификатор пользователя, сделавшего ставку 
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Навигационное свойство пользователя
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Сумма ставки
        /// </summary>
        public decimal Amount { get; set; }
        // <summary>
        /// Дата и время, когда ставка была размещена
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
