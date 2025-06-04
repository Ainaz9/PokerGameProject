using Microsoft.Extensions.DependencyInjection;
using PokerGameRSF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                OnRulesClicked = this.ShowRulesOfGameContent
            };
        }
        private MenuControlActions _menuControlActions;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

            ShowMainMenuContent();
        }

        private void ShowMainMenuContent()
        {
            contentPanel.Controls.Clear();
            var mainMenuContentControl = serviceProvider.GetRequiredService<MainMenuContentControl>();
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
