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
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; }

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
        /// Изображение пользователя
        /// </summary>
        public byte[]? Image { get; set; }

        /// <summary>
        /// Текущий рейтинг игрока
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Игры, в которых пользователь был первым игроком
        /// </summary>
        public ICollection<GameSession> GamesAsPlayer1 { get; set; }

        /// <summary>
        /// Игры, в которых пользователь был вторым игроком
        /// </summary>
        public ICollection<GameSession> GamesAsPlayer2 { get; set; }
    }
}
