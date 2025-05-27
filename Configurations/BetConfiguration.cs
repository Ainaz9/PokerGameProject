using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PokerGameRSF.Models;
namespace PokerGameRSF.Configurations
{
    /// <summary>
    /// Предоставляет конфигурацию для сущности Bet
    /// </summary>
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        /// <summary>
        /// Метод Configure применяет конфигурацию для сущности Bet
        /// </summary>
        /// <param name="builder">Объект, используемый для настройки модели</param>
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
