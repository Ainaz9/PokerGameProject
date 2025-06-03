using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public class BoardCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }
        public Guid CardId { get; set; }
        public virtual Card Card { get; set; }
        public int Position { get; set; } // 1-3: flop, 4: turn, 5: river
    }
}
