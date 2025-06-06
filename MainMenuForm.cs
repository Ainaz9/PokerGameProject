using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokerGameRSF;
using PokerGameRSF.Services;
using PokerGame.Models;
using PokerGameRSF.Models;

namespace PokerGame
{
    public partial class MainMenuForm : Form
    {
        private readonly IServiceProvider serviceProvider;

        public MainMenuForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            _menuControlActions = new MenuControlActions()
            {
                OnCrossClicked = this.CloseRightMenu, 
                OnMainMenuClicked = this.ShowMainMenuContent,
                OnGameRoomClicked = this.ShowGameRoomContent,
                OnProfileClicked = this.ShowProfileContent,
                OnRulesClicked = this.ShowRulesOfGameContent,
                OnStartGameOnePlayer = this.startGameButtonPicture_Click,
            };
        }
        private MenuControlActions _menuControlActions;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void startGameButtonPicture_Click(object sender, EventArgs e)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            
            context.GameSessions.RemoveRange(context.GameSessions);
            await context.SaveChangesAsync();

            context.Users.RemoveRange(context.Users.Where(u => u.Login.StartsWith("Player")));
            await context.SaveChangesAsync();

            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Login = "Player1",
                Email = "player1@example.com",
                Chips = 10000,
                Password = new byte[] { 0x01, 0x02, 0x03 },
                Salt = new byte[] { 0x10, 0x20, 0x30 }
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Login = "Player2",
                Email = "player2@example.com",
                Chips = 10000,
                Password = new byte[] { 0x04, 0x05, 0x06 },
                Salt = new byte[] { 0x40, 0x50, 0x60 }
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            await context.SaveChangesAsync(); 

            var gameService = scope.ServiceProvider.GetRequiredService<IGameSessionService>();
            var session = await gameService.StartGameAsync(user1.Id, user2.Id);

            var bet1 = new Bet { Amount = 0, GameSessionId = session.Id, UserId = user1.Id };
            var bet2 = new Bet { Amount = 0, GameSessionId = session.Id, UserId = user2.Id };

            context.Bets.Add(bet1);
            context.Bets.Add(bet2);
            await context.SaveChangesAsync(); 

            var form1 = new UserFormPlayerOne(user1.Id, gameService, scope.ServiceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>());
            var form2 = new UserFormPlayerTwo(user2.Id, gameService, scope.ServiceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>());

            form1.Show();
            form2.Show();
        }

        public void StarGameShow()
        {
            UserFormPlayerOne formPlayerOne = serviceProvider.GetRequiredService<UserFormPlayerOne>();
            formPlayerOne.Show();

            UserFormPlayerTwo formPlayerTwo = serviceProvider.GetRequiredService<UserFormPlayerTwo>();
            formPlayerTwo.Show();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

            ShowMainMenuContent();
        }

        private void ShowMainMenuContent()
        {
            contentPanel.Controls.Clear();
            var mainMenuContentControl = serviceProvider.GetRequiredService<MainMenuContentControl>();
            mainMenuContentControl.MenuControlActions = this._menuControlActions;
            contentPanel.Controls.Add(mainMenuContentControl);
            mainMenuContentControl.Dock = DockStyle.Fill;

        }
        private void ShowRulesOfGameContent()
        {
            contentPanel.Controls.Clear();
            var rulesOfGameControl = serviceProvider.GetRequiredService<RulesOfGame>();
            contentPanel.Controls.Add(rulesOfGameControl);
            rulesOfGameControl.Dock = DockStyle.Fill;
        }
        private void ShowGameRoomContent()
        {
            contentPanel.Controls.Clear();
            var gameRoomControl = serviceProvider.GetRequiredService<GameRoomControl>();
            contentPanel.Controls.Add(gameRoomControl);
            gameRoomControl.Dock = DockStyle.Fill;
        }
        private void ShowProfileContent()
        {
            contentPanel.Controls.Clear();
            var profileControl = serviceProvider.GetRequiredService<ProfileControl>();
            contentPanel.Controls.Add(profileControl);
            profileControl.Dock = DockStyle.Fill;
        }
        private void ShowRightMenu()
        {
            var rightMenu = serviceProvider.GetRequiredService<MenuControl>();
            this.Controls.Add(rightMenu);
            rightMenu.Dock = DockStyle.Right;
            this.Controls.SetChildIndex(rightMenu, 0);
            rightMenu.MenuControlActions = this._menuControlActions;
        } 
        private void CloseRightMenu()
        {
         
            var rightMenu = serviceProvider.GetRequiredService<MenuControl>();
            this.Controls.Remove(rightMenu);
        }



        private void menuButtonPictureBox_Click(object sender, EventArgs e)
        {
            ShowRightMenu();
        }
    }
}
