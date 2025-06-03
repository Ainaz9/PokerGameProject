using PokerGameRSF.DataAccess;
using PokerGameRSF.Services;
using PokerGameRSF.Models;
using Microsoft.EntityFrameworkCore;

namespace PokerGameRSF
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var context = new MyDbContextFactory().CreateDbContext(null))
            {
                context.Database.Migrate();
                // —оздать пользователей по умолчанию (тест)
                var user1 = context.Users.FirstOrDefault(u => u.Username == "Player1");
                if (user1 == null)
                {
                    user1 = new User { Username = "Player1" };
                    context.Users.Add(user1);
                }
                var user2 = context.Users.FirstOrDefault(u => u.Username == "Player2");
                if (user2 == null)
                {
                    user2 = new User { Username = "Player2" };
                    context.Users.Add(user2);
                }
                context.SaveChanges();
                user1 = context.Users.First(u => u.Username == "Player1");
                user2 = context.Users.First(u => u.Username == "Player2");
                // Ќачать новую игру, если нет активных
                if (!context.GameSessions.Any(gs => gs.IsActive))
                {
                    GameSessionManager.StartGame(user1.Id, user2.Id);
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new FormPlayer1());
        }
    }
    public class TwoFormApplicationContext : ApplicationContext
    {
        private int openForms;
        public TwoFormApplicationContext()
        {
            openForms = 2;
            var form1 = new FormPlayer1();
            var form2 = new FormPlayer2();
            form1.FormClosed += (s, e) => { if (--openForms <= 0) ExitThread(); };
            form2.FormClosed += (s, e) => { if (--openForms <= 0) ExitThread(); };
            form1.Show();
            form2.Show();
        }
    }
}