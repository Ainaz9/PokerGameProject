<<<<<<< HEAD
﻿using PokerGamesRSF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamesRSF.Configurations
{
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Amount).IsRequired();
            builder.HasOne(b => b.GameSession)
                   .WithMany(gs => gs.Bets)
                   .HasForeignKey(b => b.GameSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bets)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
=======
﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
>>>>>>> 7fa7f031cde161018d93048ce9c626373cf7b0b9
        }
    }
}
