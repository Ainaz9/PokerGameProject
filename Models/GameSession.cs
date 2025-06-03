using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public enum GamePhase { PreFlop, Flop, Turn, River, Showdown, Completed }

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
        public int Pot { get; set; } = 0;
        public DateTime LastActionTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<BoardCard> BoardCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
