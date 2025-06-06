using Microsoft.EntityFrameworkCore;
using PokerGame.Models;
using PokerGameRSF.DTO;
using PokerGameRSF.Models;
using PokerGameRSF.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PokerGameRSF
{
    public partial class UserFormPlayerTwo : Form
    {
        private readonly Guid _currentUserId;
        private readonly IGameSessionService _gameSessionService;
        private readonly IDbContextFactory<MyDbContext> _contextFactory;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Guid _sessionId = Guid.Empty;

        public UserFormPlayerTwo(Guid currentUserId, IGameSessionService gameSessionService, IDbContextFactory<MyDbContext> contextFactory)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
            _gameSessionService = gameSessionService;
            _contextFactory = contextFactory;

            FoldPictureBox.Click += FoldPictureBox_Click;
            CheckPictureBox.Click += CheckPictureBox_Click;
            CallPictureBox.Click += CallPictureBox_Click;
            SeeCombinationsPictureBox.Click += SeeCombinationsPictureBox_Click;
            RaisePictureBox.Click += RaisePictureBox_Click;

            Task.Run(() => PollGameStateAsync(_cancellationTokenSource.Token));
        }

        private async void RaisePictureBox_Click(object sender, EventArgs e)
        {
            if (_sessionId == Guid.Empty) return;
            using var context = await _contextFactory.CreateDbContextAsync();
            var session = await context.GameSessions.FindAsync(_sessionId);
            if (session?.CurrentTurnPlayerId != _currentUserId) return;

            string input = Interaction.InputBox("Введите сумму Raise:", "Ставка Raise", "", -1, -1);
            if (!decimal.TryParse(input, out decimal raiseAmount) || raiseAmount <= 0)
                return;

            try
            {
                await _gameSessionService.SendActionAsync(_sessionId, new ActionDto
                {
                    PlayerId = _currentUserId,
                    Action = ActionType.Raise,
                    Amount = raiseAmount
                });
                await context.SaveChangesAsync();

                MessageBox.Show("Ход выполнен. Ход передан сопернику.", "Raise", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var updatedSession = await context.GameSessions
                    .Include(g => g.Player1)
                    .Include(g => g.Player2)
                    .Include(g => g.Bets)                               
                    .Include(g => g.PlayerCards).ThenInclude(pc => pc.Card)
                    .FirstOrDefaultAsync(g => g.Id == _sessionId);
                UpdateUI(updatedSession);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при Raise: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            base.OnFormClosing(e);
        }

        private async Task PollGameStateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using var context = await _contextFactory.CreateDbContextAsync(token);

                    var session = await context.GameSessions
                        .AsNoTracking()
                        .Include(gs => gs.Player1)
                        .Include(gs => gs.Player2)
                        .Include(gs => gs.Bets)                             
                        .Include(gs => gs.PlayerCards).ThenInclude(pc => pc.Card)
                        .FirstOrDefaultAsync(gs =>
                            gs.Player1Id == _currentUserId ||
                            gs.Player2Id == _currentUserId,
                            token);

                    if (session != null)
                    {
                        _sessionId = session.Id;
                        Invoke(new Action(() => UpdateUI(session)));
                    }
                    await Task.Delay(500, token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Polling error: {ex.Message}");
                }
            }
        }

        private void UpdateUI(GameSession session)
        {
            bool isPlayerOne = session.Player1Id == _currentUserId;
            var currentPlayer = isPlayerOne ? session.Player1 : session.Player2;
            var opponent = isPlayerOne ? session.Player2 : session.Player1;

            PlayerOneSumChips.Text = currentPlayer.Chips.ToString();
            PlayerTwoSumChips.Text = opponent.Chips.ToString();

            PlayerOneBetLabel.Text = session.Bets
                .Where(b => b.UserId == _currentUserId)
                .Sum(b => b.Amount).ToString();
            PlayerTwoBetLabel.Text = session.Bets
                .Where(b => b.UserId != _currentUserId)
                .Sum(b => b.Amount).ToString();

            SumChipsPlayersReturnWinnerLabel.Text = session.Pot.ToString();

            var tableCards = session.PlayerCards.Where(pc => !pc.IsInHand).ToList();
            UpdateTableCards(tableCards);

            UpdatePlayerCards(session);

            bool isMyTurn = session.CurrentTurnPlayerId == _currentUserId;
            FoldPictureBox.Enabled = isMyTurn;
            CheckPictureBox.Enabled = isMyTurn && session.CurrentBet == 0;
            CallPictureBox.Enabled = isMyTurn && session.CurrentBet > 0;
            RaisePictureBox.Enabled = isMyTurn;
            SeeCombinationsPictureBox.Enabled = isMyTurn;

            if (session.Phase == GamePhase.Completed && session.WinnerId != null)
            {
                var winnerUser = (session.WinnerId == _currentUserId) ? session.Player2 : session.Player1;
                MessageBox.Show($"Игра окончена. Победил игрок {winnerUser.Login}. Комбинация: {session.WinningCombination}", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FoldPictureBox.Enabled = false;
                CheckPictureBox.Enabled = false;
                CallPictureBox.Enabled = false;
                RaisePictureBox.Enabled = false;
                SeeCombinationsPictureBox.Enabled = false;
            }
        }

        private void UpdateTableCards(System.Collections.Generic.List<PlayerCard> tableCards)
        {
            var cardImages = tableCards.Select(pc => GetCardImage(pc.Card)).ToList();
            FirstCardOnTablePictureBox.Image = cardImages.ElementAtOrDefault(0);
            SecondCardOnTablePictureBox.Image = cardImages.ElementAtOrDefault(1);
            ThirdCardOnTablePictureBox.Image = cardImages.ElementAtOrDefault(2);
            FourthCardOnTablePictureBox.Image = cardImages.ElementAtOrDefault(3);
            FifthCardOnTablePictureBox.Image = cardImages.ElementAtOrDefault(4);
        }

        private void UpdatePlayerCards(GameSession session)
        {
            var playerCards = session.PlayerCards
                .Where(pc => pc.UserId == _currentUserId && pc.IsInHand)
                .Select(pc => pc.Card)
                .ToList();

            PlayerOneCardOnePictureBox.Image = GetCardImage(playerCards.ElementAtOrDefault(0));
            PlayerOneCardTwoPictureBox.Image = GetCardImage(playerCards.ElementAtOrDefault(1));

            if (session.Phase >= GamePhase.Showdown)
            {
                var opponentCards = session.PlayerCards
                    .Where(pc => pc.UserId != _currentUserId && pc.IsInHand)
                    .Select(pc => pc.Card)
                    .ToList();

                PlayerTwoCardOnePictureBox.Image = GetCardImage(opponentCards.ElementAtOrDefault(0));
                PlayerTwoCardTwoPictureBox.Image = GetCardImage(opponentCards.ElementAtOrDefault(1));
            }
        }

        private async void FoldPictureBox_Click(object sender, EventArgs e)
        {
            if (_sessionId == Guid.Empty) return;

            using var context = await _contextFactory.CreateDbContextAsync();
            var session = await context.GameSessions.FindAsync(_sessionId);
            if (session?.CurrentTurnPlayerId != _currentUserId) return;

            try
            {
                await _gameSessionService.SendActionAsync(_sessionId, new ActionDto
                {
                    PlayerId = _currentUserId,
                    Action = ActionType.Fold
                });
                await context.SaveChangesAsync();

                var finishedSession = await context.GameSessions
                    .Include(g => g.Player1)
                    .Include(g => g.Player2)
                    .Include(g => g.Bets)                        
                    .FirstOrDefaultAsync(g => g.Id == _sessionId);

                var winnerUser = (finishedSession.WinnerId == _currentUserId)
                    ? finishedSession.Player2
                    : finishedSession.Player1;
                MessageBox.Show($"Игрок {winnerUser.Login} победил (противник сбросил). Комбинация: {finishedSession.WinningCombination}", "Итог", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateUI(finishedSession);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при Fold: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CheckPictureBox_Click(object sender, EventArgs e)
        {
            if (_sessionId == Guid.Empty) return;

            using var context = await _contextFactory.CreateDbContextAsync();
            var session = await context.GameSessions
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Bets)                             
                .FirstOrDefaultAsync(g => g.Id == _sessionId);

            if (session?.CurrentTurnPlayerId != _currentUserId) return;

            try
            {
                var myBet = await context.Bets
                    .Where(b => b.GameSessionId == _sessionId && b.UserId == _currentUserId)
                    .SumAsync(b => (decimal?)b.Amount) ?? 0;
                var oppBet = await context.Bets
                    .Where(b => b.GameSessionId == _sessionId && b.UserId != _currentUserId)
                    .SumAsync(b => (decimal?)b.Amount) ?? 0;

                if (myBet == oppBet)
                {
                    await _gameSessionService.SendActionAsync(_sessionId, new ActionDto
                    {
                        PlayerId = _currentUserId,
                        Action = ActionType.Check
                    });
                    await context.SaveChangesAsync();

                    MessageBox.Show("Ход выполнен. Ход передан сопернику.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await HandleCallAsync();
                    return;
                }

                var updatedSession = await context.GameSessions
                    .Include(g => g.Player1)
                    .Include(g => g.Player2)
                    .Include(g => g.Bets)                        
                    .Include(g => g.PlayerCards).ThenInclude(pc => pc.Card)
                    .FirstOrDefaultAsync(g => g.Id == _sessionId);
                UpdateUI(updatedSession);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при Check: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CallPictureBox_Click(object sender, EventArgs e)
        {
            await HandleCallAsync();
        }

        private async Task HandleCallAsync()
        {
            if (_sessionId == Guid.Empty) return;

            using var context = await _contextFactory.CreateDbContextAsync();
            var session = await context.GameSessions.FindAsync(_sessionId);
            if (session?.CurrentTurnPlayerId != _currentUserId) return;

            try
            {
                await _gameSessionService.SendActionAsync(_sessionId, new ActionDto
                {
                    PlayerId = _currentUserId,
                    Action = ActionType.Call
                });
                await context.SaveChangesAsync();

                MessageBox.Show("Ход выполнен. Ход передан сопернику.", "Call", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var updatedSession = await context.GameSessions
                    .Include(g => g.Player1)
                    .Include(g => g.Player2)
                    .Include(g => g.Bets)                        
                    .Include(g => g.PlayerCards).ThenInclude(pc => pc.Card)
                    .FirstOrDefaultAsync(g => g.Id == _sessionId);
                UpdateUI(updatedSession);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при Call: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SeeCombinationsPictureBox_Click(object sender, EventArgs e)
        {
            if (_sessionId == Guid.Empty) return;

            using var context = await _contextFactory.CreateDbContextAsync();
            var session = await context.GameSessions
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Bets)                             
                .Include(g => g.PlayerCards).ThenInclude(pc => pc.Card)
                .FirstOrDefaultAsync(g => g.Id == _sessionId);

            if (session == null || session.CurrentTurnPlayerId != _currentUserId) return;

            UpdateUI(session);
        }

        private Image GetCardImage(Card card)
        {
            try
            {
                if (card == null)
                    return Image.FromFile("Images/Cards/back.png");
                string path = $"Images/Cards/{card.Suit}_{card.Rank}.png";
                return Image.FromFile(path);
            }
            catch
            {
                return Image.FromFile("Images/Cards/unknown.png");
            }
        }
    }
}
