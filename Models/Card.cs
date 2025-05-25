namespace PokerGameRSF.Models
{
    public class Card
    {
        public Guid Id { get; set; } 
        public CardSuit Suit { get; set; } 
        public CardRank Rank { get; set; } 
    }

}
