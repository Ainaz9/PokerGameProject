using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Действия игрока
    /// </summary>
    public class PlayerAction
    {
        /// <summary>
        /// Уникальный идентификатор действия 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор игровой сессии
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Игровая сессия, в которой было действие 
        /// </summary>
        public GameSession GameSession { get; set; }

        /// <summary>
        /// Уникальный идентификатор игрока совершающий действие 
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Игрок, который выполнил действие 
        /// </summary>
        public User Player { get; set; }

        /// <summary>
        /// Действие игрока
        /// </summary>
        public ActionType Action { get; set; }

        /// <summary>
        /// Необязательное числовое значение, используется только для действий, требующих указания суммы
        /// </summary>
        public decimal? Amount { get; set; } // для raise/call

        /// <summary>
        /// Дата и время выполнения действия 
        /// </summary>
        public DateTime PerformedAt { get; set; }
    }
}
