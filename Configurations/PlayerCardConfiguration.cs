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
