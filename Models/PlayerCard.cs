using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public class PlayerCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
