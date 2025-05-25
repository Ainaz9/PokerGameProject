using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Ставки
    /// </summary>
    public class Bet
    {
        public Guid Id { get; set; }

        public Guid GameSessionId { get; set; }
        public GameSession GameSession { get; set; }

        public Guid PlayerId { get; set; }
        public User Player { get; set; }

        public decimal Amount { get; set; }

        public DateTime PlacedAt { get; set; }
    }

}
