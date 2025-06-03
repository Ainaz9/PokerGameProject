using PokerGameRSF.Configurations;
using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;

namespace PokerGameRSF.DataAccess
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<BoardCard> BoardCards { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<PlayerAction> PlayerActions { get; set; }
        public DbSet<Bet> Bets { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.Migrate();
            if (this.Cards != null && !this.Cards.Any())
            {
                var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
                for (int rank = 2; rank <= 14; rank++)
                {
                    foreach (var suit in suits)
                    {
                        this.Cards.Add(new Card { Rank = rank, Suit = suit });
                    }
                }
                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=PokerDB;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerCardConfiguration());
            modelBuilder.ApplyConfiguration(new BoardCardConfiguration());
            modelBuilder.ApplyConfiguration(new GameSessionConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerActionConfiguration());
            modelBuilder.ApplyConfiguration(new BetConfiguration());
        }
    }
}
