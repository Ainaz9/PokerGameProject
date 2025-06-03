using PokerGameRSF.DataAccess;
using PokerGameRSF.Models;
using PokerGameRSF.Services;
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
    public partial class FormPlayer2 : Form
    {
        private Guid myUserId;
        private Guid opponentUserId;
        private System.Windows.Forms.Timer refreshTimer;
        private const int TurnTimeoutSeconds = 30;

        public FormPlayer2()
        {
            InitializeComponent();
            // Определяем идентификаторы пользователей
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var me = context.Users.FirstOrDefault(u => u.Username == "Player2");
                var opp = context.Users.FirstOrDefault(u => u.Username == "Player1");
                if (me == null || opp == null)
                {
                    MessageBox.Show("Users not found.");
                    this.Close();
                    return;
                }
                myUserId = me.Id;
                opponentUserId = opp.Id;
            }
            // Таймер обновления пользовательского интерфейса
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 500;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                var session = context.GameSessions.FirstOrDefault(gs => gs.IsActive);
                if (session == null)
                {
                    lblTurn.Text = "Game Over";
                    btnFold.Enabled = btnCheck.Enabled = btnCall.Enabled = btnRaise.Enabled = false;
                    return;
                }
                // Показать карты игрока
                var cards = context.PlayerCards
                    .Where(pc => pc.GameSessionId == session.Id && pc.UserId == myUserId)
                    .Select(pc => pc.Card)
                    .ToList();
                if (cards.Count >= 2)
                {
                    lblCard1.Text = $"{cards[0].Rank} of {cards[0].Suit}";
                    lblCard2.Text = $"{cards[1].Rank} of {cards[1].Suit}";
                }
                // Показывать общие карты в зависимости от фазы
                var community = context.BoardCards
                    .Where(bc => bc.GameSessionId == session.Id &&
                         bc.Position <= (session.Phase == GamePhase.PreFlop ? 0 :
                                        session.Phase == GamePhase.Flop ? 3 :
                                        session.Phase == GamePhase.Turn ? 4 :
                                        session.Phase == GamePhase.River ? 5 : 5))
                    .OrderBy(bc => bc.Position)
                    .Select(bc => bc.Card)
                    .ToList();
                lblPot.Text = $"Pot: {session.Pot}";
                // обн стека
                var me = context.Users.Find(myUserId);
                var opp = context.Users.Find(opponentUserId);
                lblMyStack.Text = $"Your Chips: {me.Chips}";
                lblOppStack.Text = $"Opponent Chips: {opp.Chips}";
                // Текущий ход и таймер
                bool myTurn = (session.CurrentTurnPlayerId == myUserId);
                if (myTurn)
                {
                    lblTurn.Text = "Your Turn";
                    btnFold.Enabled = btnCheck.Enabled = btnCall.Enabled = btnRaise.Enabled = true;
                    nudRaise.Enabled = true;
                    var elapsed = (DateTime.UtcNow - session.LastActionTime).TotalSeconds;
                    int secondsLeft = (int)(TurnTimeoutSeconds - elapsed);
                    if (secondsLeft < 0) secondsLeft = 0;
                    lblTimer.Text = $"Time left: {secondsLeft}s";
                    if (secondsLeft == 0)
                    {
                        // авто fold при таймере
                        GameSessionManager.HandleTimeout(myUserId);
                    }
                }
                else
                {
                    lblTurn.Text = "Waiting for opponent...";
                    btnFold.Enabled = btnCheck.Enabled = btnCall.Enabled = btnRaise.Enabled = false;
                    nudRaise.Enabled = false;
                    var elapsed = (DateTime.UtcNow - session.LastActionTime).TotalSeconds;
                    int secondsLeft = (int)(TurnTimeoutSeconds - elapsed);
                    if (secondsLeft < 0) secondsLeft = 0;
                    lblTimer.Text = $"Opponent time left: {secondsLeft}s";
                }
            }
        }
    }
}
