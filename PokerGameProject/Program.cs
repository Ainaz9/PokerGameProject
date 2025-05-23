using Microsoft.Extensions.DependencyInjection;

namespace PokerGameProject
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var sc = new ServiceCollection();
            sc.AddSingleton<RegisterForm>();
            sc.AddSingleton<LoginForm>();
            sc.AddSingleton<MainMenuForm>();
            sc.AddSingleton<App>();
            sc.AddSingleton<AuthContainer>();
            var serviceProvider = sc.BuildServiceProvider();
            var app = serviceProvider.GetRequiredService<App>();
            Application.Run(app);
            
            
        }
    }
}