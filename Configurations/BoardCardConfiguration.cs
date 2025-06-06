using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameRSF.Configurations
{
    public class BoardCardConfiguration : IEntityTypeConfiguration<BoardCard>
    {
        public void Configure(EntityTypeBuilder<BoardCard> builder)
        {
            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.Position).IsRequired();
            builder.HasOne(bc => bc.GameSession)
                   .WithMany(gs => gs.BoardCards).HasForeignKey(bc => bc.GameSessionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(bc => bc.Card)
                   .WithMany(c => c.BoardCards).HasForeignKey(bc => bc.CardId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
