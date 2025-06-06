using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;

namespace PokerGameRSF.Configurations
{
    /// <summary>
    /// Предоставляет конфигурацию для сущности Card
    /// </summary>
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        /// <summary>
        /// Метод Configure применяет конфигурацию для сущности Card
        /// </summary>
        /// <param name="builder">Объект, используемый для настройки модели</param>
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Rank).IsRequired();

            builder.Property(c => c.Suit).IsRequired();

            builder.HasMany(c => c.PlayerCards)
                   .WithOne(pc => pc.Card)
                   .HasForeignKey(pc => pc.CardId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
