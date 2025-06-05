using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerGameRSF
{
    public partial class MainMenuContentControl : UserControl
    {
        public MainMenuContentControl()
        {
            InitializeComponent();
        }

        private void startgameLabel_Click(object sender, EventArgs e)
        {
            StartGameButtonClick();
        }

        private void startGameButtonPicture_Click(object sender, EventArgs e)
        {
            StartGameButtonClick();
        }
        private void StartGameButtonClick()
        {

        }
    }
}
