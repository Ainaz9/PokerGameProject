using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public int Rating { get; set; } = 0;
        public int Chips { get; set; } = 1000;
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<PlayerAction> PlayerActions { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer1 { get; set; }
        public virtual ICollection<GameSession> GameSessionsAsPlayer2 { get; set; }
    }
}
