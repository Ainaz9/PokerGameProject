using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameRSF.Configurations
{
    public class PlayerActionConfiguration : IEntityTypeConfiguration<PlayerAction>
    {
        public void Configure(EntityTypeBuilder<PlayerAction> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder.HasOne(pa => pa.GameSession)
                   .WithMany(gs => gs.PlayerActions).HasForeignKey(pa => pa.GameSessionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pa => pa.User)
                   .WithMany(u => u.PlayerActions).HasForeignKey(pa => pa.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(pa => pa.ActionType).IsRequired();
            builder.HasOne(pa => pa.Bet)
                   .WithOne().HasForeignKey<PlayerAction>(pa => pa.BetId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
