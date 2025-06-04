using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.DTO
{
    public class ActionDto
    {
        public Guid PlayerId { get; set; }
        public ActionType Action { get; set; }
        public decimal? Amount { get; set; }
    }
}
