namespace PokerGamesRSF.Models
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
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Уникальный идентификатор игровой сессии, в которой используется карта
        /// </summary>
        public Guid GameSessionId { get; set; }
        /// <summary>
        /// Навигационный объект игровой сессии
        /// </summary>
        public virtual GameSession GameSession { get; set; }
        /// <summary>
        /// Уникальный идентификатор пользователя, которому принадлежит карта
        /// </summary>
        public Guid? UserId { get; set; } // Null для community cards
        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Уникальный идентификатор карты
        /// </summary>
        public Guid CardId { get; set; }
        /// <summary>
        /// Навигационный объект карты
        /// </summary>
        public virtual Card Card { get; set; }
        /// <summary>
        /// Логическое значение, указывающее, находится ли карта в руке у игрока (true) или на столе (false)
        /// </summary>
        public bool IsInHand { get; set; } 
    }
}
