using Microsoft.EntityFrameworkCore;
using PokerGame.Models;
using PokerGameRSF.Configurations;
namespace PokerGameRSF
{

    // Взаимодействие с базой данных в Entity Framework Core происходит посредством специального класса - контекста данных.
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }

        public DbSet<GameSession> Session { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameSessionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Pokerdb;Username=postgres;Password=postgres");
            }
        }
    }
}
