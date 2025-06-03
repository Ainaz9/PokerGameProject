using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameRSF.Configurations
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasKey(gs => gs.Id);
            builder.Property(gs => gs.Phase).IsRequired();
            builder.Property(gs => gs.Pot);//.HasDefaultValue(0);
            builder.Property(gs => gs.IsActive);//.HasDefaultValue(true);

            builder.HasOne(gs => gs.Player1)
                   .WithMany(u => u.GameSessionsAsPlayer1).HasForeignKey(gs => gs.Player1Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(gs => gs.Player2)
                   .WithMany(u => u.GameSessionsAsPlayer2).HasForeignKey(gs => gs.Player2Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(gs => gs.CurrentTurnPlayer)
                   .WithMany().HasForeignKey(gs => gs.CurrentTurnPlayerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
