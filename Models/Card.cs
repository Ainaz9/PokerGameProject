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
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Сила карты
        /// </summary>
        public int Rank { get; set; } // 2-14 (11=J,12=Q,13=K,14=A)
        /// <summary>
        /// Масть карты
        /// </summary>
        public string Suit { get; set; } // Hearts, Diamonds, Clubs, Spades
        /// <summary>
        /// Связь с PlayerCards
        /// </summary>
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }

}
