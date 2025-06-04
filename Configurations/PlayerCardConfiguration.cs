using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.Configurations
{
    public class PlayerCardConfiguration : IEntityTypeConfiguration<PlayerCard>
    {
        public void Configure(EntityTypeBuilder<PlayerCard> builder)
        {
            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.IsInHand).IsRequired();
            builder.Property(pc => pc.UserId).IsRequired(false); // Nullable for community cards
            builder.HasOne(pc => pc.GameSession)
                   .WithMany(gs => gs.PlayerCards)
                   .HasForeignKey(pc => pc.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pc => pc.User)
                   .WithMany(u => u.PlayerCards)
                   .HasForeignKey(pc => pc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pc => pc.Card)
                   .WithMany(c => c.PlayerCards)
                   .HasForeignKey(pc => pc.CardId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
