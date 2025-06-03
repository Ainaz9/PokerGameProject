using PokerGameRSF.Models;
namespace PokerGameRSF.Server
{
    /// <summary>
    /// Модель сообщения.
    /// </summary>
    public class GameMessage
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid PlayerID { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string PlayerLogin {  get; set; }
        /// <summary>
        /// Ставка пользователя
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Время действия
        /// </summary>
        public DateTime PerformedAt { get; set; }
        /// <summary>
        /// Действие пользователя
        /// </summary>
        public ActionType Type { get; set; }
    }
}
