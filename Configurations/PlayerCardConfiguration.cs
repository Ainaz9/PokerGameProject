using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;

namespace PokerGameRSF.Configurations
{
    /// <summary>
    /// Предоставляет конфигурацию для сущности PlayerCard
    /// </summary>
    public class PlayerCardConfiguration : IEntityTypeConfiguration<PlayerCard>
    {  
        /// <summary>
        /// Метод Configure применяет конфигурацию для сущности PlayerCard
        /// </summary>
        /// <param name="builder">Объект, используемый для настройки модели</param>
        public void Configure(EntityTypeBuilder<PlayerCard> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.IsInHand).IsRequired();
            builder.Property(pc => pc.UserId).IsRequired(false); // Nullable для community cards

            builder.HasOne(pc => pc.GameSession)
                   .WithMany(gs => gs.PlayerCards)
                   .HasForeignKey(pc => pc.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.User)
                   .WithMany(u => u.PlayerCards)
                   .HasForeignKey(pc => pc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.Card)
                   .WithMany(c => c.PlayerCards)
                   .HasForeignKey(pc => pc.CardId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pc => new { pc.GameSessionId, pc.UserId });
        }
    }
}
