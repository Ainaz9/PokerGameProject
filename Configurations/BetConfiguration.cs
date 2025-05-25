using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;
namespace PokerGameRSF.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.GameSession)
                .WithMany()
                .HasForeignKey(b => b.GameSessionId);


            builder.HasOne(b => b.Player)
                .WithMany()
                .HasForeignKey(b => b.PlayerId);
        }
    }
}
