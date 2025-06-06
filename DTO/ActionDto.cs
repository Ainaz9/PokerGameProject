using PokerGameRSF.Models;

namespace PokerGameRSF.DTO
{
    /// <summary>
    /// Класс для представления действия игрока
    /// </summary>
    public class ActionDto
    {
        /// <summary>
        /// Уникальный Id игрока
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Действие игрока
        /// </summary>
        public ActionType Action { get; set; }
        /// <summary>
        /// Ставка игрока
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
