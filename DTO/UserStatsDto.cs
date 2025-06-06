namespace PokerGameRSF.DTO
{
    /// <summary>
    /// Класс для представления статистики игрока
    /// </summary>
    public class UserStatsDto
    {
        /// <summary>
        /// Рейтинг игрока
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Лучший выигрыш
        /// </summary>
        public decimal BestWinningAmount { get; set; }
        /// <summary>
        /// Лучшая комбинация
        /// </summary>
        public string BestCombination { get; set; }
    }
}
