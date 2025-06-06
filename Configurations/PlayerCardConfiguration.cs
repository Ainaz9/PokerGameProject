using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameRSF.Configurations
{
    public class PlayerCardConfiguration : IEntityTypeConfiguration<PlayerCard>
    {
        public void Configure(EntityTypeBuilder<PlayerCard> builder)
        {
            builder.HasKey(pc => pc.Id);
            builder.HasOne(pc => pc.GameSession)
                   .WithMany(gs => gs.PlayerCards).HasForeignKey(pc => pc.GameSessionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pc => pc.User)
                   .WithMany(u => u.PlayerCards).HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pc => pc.Card)
                   .WithMany(c => c.PlayerCards).HasForeignKey(pc => pc.CardId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
