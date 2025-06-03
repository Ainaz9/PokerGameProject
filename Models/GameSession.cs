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
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор первого игрока
        /// </summary>
        public Guid PlayerId1 { get; set; }

        /// <summary>
        /// Объект первого игрока
        /// </summary>
        public User Player1 { get; set; }

        /// <summary>
        /// Идентификатор второго игрока
        /// </summary>
        public Guid PlayerId2 { get; set; }

        /// <summary>
        /// Объект второго игрока
        /// </summary>
        public User Player2 { get; set; }

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Дата и время окончания игры (если завершена)
        /// </summary>
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// Идентификатор победителя
        /// </summary>
        public Guid? WinnerId { get; set; }

        /// <summary>
        /// Объект победителя (если определён)
        /// </summary>
        public User? Winner { get; set; }
    }
}
