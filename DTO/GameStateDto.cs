using PokerGameRSF.Models;

namespace PokerGameRSF.DTO
{
    /// <summary>
    /// Класс для представления состояния игры
    /// </summary>
    public class GameStateDto
    {
        /// <summary>
        /// Карты игроков
        /// </summary>
        public List<CardDto> PocketCards { get; set; }
        /// <summary>
        /// Карты стола
        /// </summary>
        public List<CardDto> CommunityCards { get; set; }
        /// <summary>
        /// Банк
        /// </summary>
        public decimal Pot { get; set; }
        /// <summary>
        /// Текущая ставка игрока
        /// </summary>
        public decimal CurrentBet { get; set; }
        public decimal PlayerStack { get; set; }
        public decimal OpponentStack { get; set; }
        /// <summary>
        /// Игровая фаза
        /// </summary>
        public GamePhase Phase { get; set; }
        /// <summary>
        /// Ход игрока
        /// </summary>
        public bool IsPlayerTurn { get; set; }
        /// <summary>
        /// Действия
        /// </summary>
        public List<string> AvailableActions { get; set; }
        /// <summary>
        /// Лучшая комбинация
        /// </summary>
        public string BestCombination { get; set; }
        /// <summary>
        /// Лучший выигрыш
        /// </summary>
        public decimal BestWinningAmount { get; set; }
    }
}
