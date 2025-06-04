using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Amount).IsRequired();
            builder.HasOne(b => b.GameSession)
                   .WithMany(gs => gs.Bets)
                   .HasForeignKey(b => b.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bets)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
