using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Представляет карту, которая принадлежит игроку в рамках игровой сессии.
    /// Используется для отслеживания всех карт, которые находятся у игрока в руке или на столе.
    /// </summary>
    public class PlayerCard
    {
        /// <summary>
        /// Уникальный идентификатор карты 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор игровой сессии, в которой используется карта
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Игровая сессия, в которой используется карта
        /// </summary>
        public GameSession GameSession { get; set; }
        /// <summary>
        /// Уникальный идентификатор пользователя, которому принадлежит карта
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Пользователь, которому принадлежит карта
        /// </summary>
        public User Player { get; set; }
        /// <summary>
        /// Уникальный идентификатор карты
        /// </summary>
        public Guid CardId { get; set; }
        /// <summary>
        /// Объект карты, представляющая масть и силу карты
        /// </summary>
        public Card Card { get; set; }
        /// <summary>
        /// Логическое значение, указывающее, находится ли карта в руке у игрока (true) или на столе (false)
        /// </summary>
        public bool IsInHand { get; set; } 
    }

}
