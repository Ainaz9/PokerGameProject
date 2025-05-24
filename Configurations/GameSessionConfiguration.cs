using Microsoft.EntityFrameworkCore;
using PokerGame.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PokerGameRSF.Configurations
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasKey(gs  => gs.Id);

            builder.HasOne(gs => gs.Player1)
            .WithMany(u => u.GamesAsPlayer1)
            .HasForeignKey(gs => gs.PlayerId1);

            builder.
            HasOne(gs => gs.Player2)
            .WithMany(u => u.GamesAsPlayer2)
            .HasForeignKey(gs => gs.PlayerId2);

            builder.HasOne(gs => gs.Winner)
            .WithMany()
            .HasForeignKey(gs => gs.WinnerId)
            .IsRequired(false);
        }
    }
}
