using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.Models
{
    public class Card
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Rank { get; set; } // 2-14 (11=J,12=Q,13=K,14=A)
        public string Suit { get; set; } // Hearts, Diamonds, Clubs, Spades
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}
