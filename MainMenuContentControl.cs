using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PokerGameRSF
{
    public partial class MainMenuContentControl : UserControl
    {
        private IDbContextFactory<MyDbContext> _dbContextFactory;
        public MenuControlActions MenuControlActions { get; set; }

        public MainMenuContentControl()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Pokerdb;Username=postgres;Password=postgres");

            _dbContextFactory = new PooledDbContextFactory<MyDbContext>(optionsBuilder.Options);
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            MenuControlActions?.OnStartGameOnePlayer?.Invoke(sender, e);
        }

    }
}
