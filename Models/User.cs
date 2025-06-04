namespace PokerGamesRSF.Models
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
        public string Username { get; set; }
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
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer1 { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer2 { get; set; }
    }
}
