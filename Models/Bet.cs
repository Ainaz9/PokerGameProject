using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Ставка пользователя, сделанная в период игровой сессии
    /// </summary>
    public class Bet
    {
        /// <summary>
        /// Уникальный идентификатор ставки
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Уникальный идентификатор игровой сессии, к которой относится ставка
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Игровая сессия, в которой была сделана ставка
        /// </summary>
        public GameSession GameSession { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя, сделавшего ставку 
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Пользователь, который делал ставку 
        /// </summary>
        public User Player { get; set; }

        /// <summary>
        /// Сумма ставки
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата и время, когда ставка была размещена
        /// </summary>
        public DateTime PlacedAt { get; set; }
    }

}
