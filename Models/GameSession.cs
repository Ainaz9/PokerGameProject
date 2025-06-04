namespace PokerGamesRSF.Models
{
    public class GameSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Player1Id { get; set; }
        public virtual User Player1 { get; set; }
        public Guid Player2Id { get; set; }
        public virtual User Player2 { get; set; }
        public Guid? CurrentTurnPlayerId { get; set; }
        public virtual User CurrentTurnPlayer { get; set; }
        public GamePhase Phase { get; set; } = GamePhase.PreFlop;
        public decimal Pot { get; set; } = 0;
        public decimal CurrentBet { get; set; } = 0;
        public DateTime LastActionTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Guid? WinnerId { get; set; }
        public virtual User Winner { get; set; }
        public string WinningCombination { get; set; }

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
