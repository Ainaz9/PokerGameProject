using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.Configurations
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasKey(gs => gs.Id);
            builder.Property(gs => gs.Phase).IsRequired();
            builder.Property(gs => gs.Pot).IsRequired();
            builder.Property(gs => gs.CurrentBet).IsRequired();
            builder.Property(gs => gs.LastActionTime).IsRequired();
            builder.Property(gs => gs.IsActive).IsRequired();

            builder.HasOne(gs => gs.Player1)
                   .WithMany(u => u.GameSessionsAsPlayer1)
                   .HasForeignKey(gs => gs.Player1Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(gs => gs.Player2)
                   .WithMany(u => u.GameSessionsAsPlayer2)
                   .HasForeignKey(gs => gs.Player2Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(gs => gs.CurrentTurnPlayer)
                   .WithMany()
                   .HasForeignKey(gs => gs.CurrentTurnPlayerId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(gs => gs.Winner)
                   .WithMany()
                   .HasForeignKey(gs => gs.WinnerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(gs => gs.PlayerCards)
                   .WithOne(pc => pc.GameSession)
                   .HasForeignKey(pc => pc.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(gs => gs.PlayerActions)
                   .WithOne(pa => pa.GameSession)
                   .HasForeignKey(pa => pa.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(gs => gs.Bets)
                   .WithOne(b => b.GameSession)
                   .HasForeignKey(b => b.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
