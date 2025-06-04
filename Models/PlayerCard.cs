namespace PokerGamesRSF.Models
{
    public class PlayerCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }
        public Guid? UserId { get; set; } // Null for community cards
        public virtual User User { get; set; }
        public Guid CardId { get; set; }
        public virtual Card Card { get; set; }
        public bool IsInHand { get; set; } // True if pocket card, false if community card
    }
}
