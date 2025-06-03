using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public class Bet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
