using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerGame.Models;

namespace PokerGameRSF.Configurations
{
    /// <summary>
    /// Предоставляет конфигурацию для сущности User
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Метод Configure применяет конфигурацию для сущности User
        /// </summary>
        /// <param name="builder">Объект, используемый для настройки модели</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u  => u.Id);

            builder.HasMany(u => u.GamesAsPlayer1)
            .WithOne(gs => gs.Player1)
            .HasForeignKey(gs => gs.PlayerId1);

            builder.HasMany( u => u.GamesAsPlayer2)
            .WithOne(gs => gs.Player2)
            .HasForeignKey(gs => gs.PlayerId2);
        }
    }
}
