// File: PokerServices/Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokerData;
using PokerData.Models;
using PokerGamesRSF.DTO;
using PokerGamesRSF.Services;
using PokerGamesRSF;
using PokerServices.DTO;

namespace PokerGamesRSF.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<MyDbContext> _contextFactory;

        public UserService(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<UserStatsDto> GetUserStatsAsync(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            // Best winning amount: max pot from sessions where user won
            var bestWin = await context.GameSessions
                .Where(gs => gs.WinnerId == userId)
                .Select(gs => gs.Pot)
                .DefaultIfEmpty(0)
                .MaxAsync();

            // Best combination: among games user won, pick highest ranking combination
            var combinations = await context.GameSessions
                .Where(gs => gs.WinnerId == userId && gs.WinningCombination != null)
                .Select(gs => gs.WinningCombination)
                .ToListAsync();

            string bestCombo = null;
            if (combinations.Any())
            {
                // Define order of poker hands
                var ranks = new Dictionary<string, int>
                {
                    { "High Card", 0 },
                    { "One Pair", 1 },
                    { "Two Pair", 2 },
                    { "Three of a Kind", 3 },
                    { "Straight", 4 },
                    { "Flush", 5 },
                    { "Full House", 6 },
                    { "Four of a Kind", 7 },
                    { "Straight Flush", 8 },
                    { "Royal Flush", 9 }
                };
                int maxRank = -1;
                foreach (var combo in combinations)
                {
                    if (ranks.TryGetValue(combo, out int r) && r > maxRank)
                    {
                        maxRank = r;
                        bestCombo = combo;
                    }
                }
            }

            return new UserStatsDto
            {
                Rating = user.Rating,
                BestWinningAmount = bestWin,
                BestCombination = bestCombo
            };
        }
    }
}
