using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Действия игрока
    /// </summary>
    public class PlayerAction
    {
        public Guid Id { get; set; }

        public Guid GameSessionId { get; set; }
        public GameSession GameSession { get; set; }

        public Guid PlayerId { get; set; }
        public User Player { get; set; }

        public ActionType Action { get; set; }

        public decimal? Amount { get; set; } // для raise/call

        public DateTime PerformedAt { get; set; }
    }
}
