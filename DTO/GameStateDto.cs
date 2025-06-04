using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.DTO
{
    public class GameStateDto
    {
        public List<CardDto> PocketCards { get; set; }
        public List<CardDto> CommunityCards { get; set; }
        public decimal Pot { get; set; }
        public decimal CurrentBet { get; set; }
        public decimal PlayerStack { get; set; }
        public decimal OpponentStack { get; set; }
        public GamePhase Phase { get; set; }
        public bool IsPlayerTurn { get; set; }
        public List<string> AvailableActions { get; set; }
        public string BestCombination { get; set; }
        public decimal BestWinningAmount { get; set; }
    }
}
