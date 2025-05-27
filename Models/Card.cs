namespace PokerGameRSF.Models
{
    /// <summary>
    /// Модель карты
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Уникальный идентификатор карты
        /// </summary>
        public Guid Id { get; set; } 
        /// <summary>
        /// Масть карты
        /// </summary>
        public CardSuit Suit { get; set; }
        /// <summary>
        /// Сила карты
        /// </summary>
        public CardRank Rank { get; set; } 
    }

}
