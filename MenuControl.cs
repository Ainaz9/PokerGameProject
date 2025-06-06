using PokerGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerGameRSF
{
    public partial class MenuControl : UserControl
    {
      
        public MenuControl()
        {
            InitializeComponent();
        }
        public MenuControlActions MenuControlActions { get; set; }

        private void CrossPictureBox_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnCrossClicked();
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnMainMenuClicked();
        }

        private void gameRoomButton_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnGameRoomClicked();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnProfileClicked();
        }

        private void rulesButton_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnRulesClicked();
        }xa

        private void exitButton_Click(object sender, EventArgs e)
        {
            MenuControlActions.OnExitFromAccountClicked();
        }
    }
    public class MenuControlActions
    {
        public Action OnCrossClicked { get; set; }
        public Action OnMainMenuClicked { get; set; }
        public Action OnProfileClicked { get; set; }
        public Action OnGameRoomClicked { get; set; }
        public Action OnRulesClicked { get; set; }
        public Action OnExitFromAccountClicked { get; set; }

       
    }
}
