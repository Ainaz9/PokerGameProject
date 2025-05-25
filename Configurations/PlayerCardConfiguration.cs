using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;
namespace PokerGameRSF.Configurations
{
    public class PlayerCardConfiguration : IEntityTypeConfiguration<PlayerCard>
    {
        public void Configure(EntityTypeBuilder<PlayerCard> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.HasOne(pc => pc.GameSession)
                .WithMany()
                .HasForeignKey(pc => pc.GameSessionId);


            builder.HasOne(pc => pc.Player)
                .WithMany()
                .HasForeignKey(pc => pc.PlayerId);


            builder.HasOne(pc => pc.Card)
                .WithMany()
                .HasForeignKey(pc => pc.CardId);
                
        }
    }
}
