using PokerGame.Models;
namespace PokerGameRSF.Models
{
    /// <summary>
    /// Карты игрока
    /// </summary>
    public class PlayerCard
    {
        public Guid Id { get; set; }

        public Guid GameSessionId { get; set; }
        public GameSession GameSession { get; set; }

        public Guid PlayerId { get; set; }
        public User Player { get; set; }

        public Guid CardId { get; set; }
        public Card Card { get; set; }

        public bool IsInHand { get; set; } // true — личная карта, false — на столе
    }

}
