namespace PokerGameRSF.DTO
{
    /// <summary>
    /// Класс для представления карты игрока
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Сила карты
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// Масть карты
        /// </summary>
        public string Suit { get; set; }
    }
}
