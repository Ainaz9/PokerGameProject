using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PokerGameRSF.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.GameSession)
                   .WithMany(gs => gs.Bets).HasForeignKey(b => b.GameSessionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bets).HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(b => b.Amount).IsRequired();
        }
    }
}
