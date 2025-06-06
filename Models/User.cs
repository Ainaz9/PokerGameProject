using PokerGameRSF.Models;

namespace PokerGame.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Текущий рейтинг игрока
        /// </summary>
        public int Rating { get; set; } = 0;
        public decimal Chips { get; set; } = 10000; // Starting chips
        /// <summary>
        /// Хэш пароля
        /// </summary>
        public byte[] Password { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Соль для хэширования пароля
        /// </summary>
        public byte[] Salt { get; set; }

        /// <summary>
        /// Действие игрока
        /// </summary>
        public PlayerAction PlayerAction { get; set; }
        /// <summary>
        /// Связь с PlayerCard
        /// </summary>
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        /// <summary>
        /// Связь с PlayerActions
        /// </summary>
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        /// <summary>
        /// Связь с Bet
        /// </summary>
        public virtual ICollection<Bet> Bets { get; set; } = new List<Bet>();
        /// <summary>
        /// Связь с GameSession
        /// </summary>
        public virtual ICollection<GameSession> GameSessionsAsPlayer1 { get; set; }
        /// <summary>
        /// Связь с GameSession
        /// </summary>
        public virtual ICollection<GameSession> GameSessionsAsPlayer2 { get; set; }
    }
}
