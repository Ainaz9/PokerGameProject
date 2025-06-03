using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public enum ActionType { Fold, Check, Call, Raise }

    public class PlayerAction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Guid? BetId { get; set; }
        public virtual Bet Bet { get; set; }
    }
}
