namespace PokerGamesRSF.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public int Rating { get; set; } = 0;
        public decimal Chips { get; set; } = 10000; // Starting chips
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer1 { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer2 { get; set; }
    }
}
