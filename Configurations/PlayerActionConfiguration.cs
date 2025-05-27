using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;
namespace PokerGameRSF.Configurations
{
    /// <summary>
    /// Предоставляет конфигурацию для сущности PlayerAction
    /// </summary>
    public class PlayerActionConfiguration : IEntityTypeConfiguration<PlayerAction>
    {
        /// <summary>
        /// Метод Configure применяет конфигурацию для сущности PlayerAction
        /// </summary>
        /// <param name="builder">Объект, используемый для настройки модели</param>
        public void Configure(EntityTypeBuilder<PlayerAction> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.HasOne(pa => pa.GameSession)
                .WithMany()
                .HasForeignKey(pa => pa.GameSessionId);


            builder.HasOne(pa => pa.Player)
                .WithMany()
                .HasForeignKey(pa => pa.PlayerId);
               
        }
    }
}
