using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokerGame;
using PokerGameProject;
using PokerGameRSF;
using PokerGameRSF.Models;
using System;

namespace PokerGame
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var services = new ServiceCollection();
            services.AddDbContext<MyDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<RegisterForm>();
            services.AddSingleton<LoginForm>();
            services.AddSingleton<MainMenuForm>();
            services.AddSingleton<App>();
            services.AddSingleton<AuthContainer>();
            services.AddSingleton<Logger>();
            ServiceProvider = services.BuildServiceProvider();
            ApplicationConfiguration.Initialize();
            var app = ServiceProvider.GetRequiredService<App>();
            Application.Run(app);

          
           
        }
        
    }
}