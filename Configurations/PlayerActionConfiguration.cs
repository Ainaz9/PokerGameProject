using PokerGamesRSF.Models;

namespace PokerGamesRSF.Configurations
{
    public class PlayerActionConfiguration : IEntityTypeConfiguration<PlayerAction>
    {
        public void Configure(EntityTypeBuilder<PlayerAction> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder.Property(pa => pa.ActionType).IsRequired();
            builder.Property(pa => pa.Timestamp).IsRequired();
            builder.HasOne(pa => pa.GameSession)
                   .WithMany(gs => gs.PlayerActions)
                   .HasForeignKey(pa => pa.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pa => pa.User)
                   .WithMany(u => u.PlayerActions)
                   .HasForeignKey(pa => pa.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pa => pa.Bet)
                   .WithOne()
                   .HasForeignKey<PlayerAction>(pa => pa.BetId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
