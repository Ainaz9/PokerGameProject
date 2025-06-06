using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using PokerGame;
using PokerGameProject;
using PokerGameRSF;
using PokerGameRSF.Models;
using PokerGameRSF.Services;
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
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IGameSessionService, GameSessionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IDbContextFactory<MyDbContext>, PooledDbContextFactory<MyDbContext>>();
            services.AddSingleton<RegisterForm>();
            services.AddSingleton<LoginForm>();
            services.AddSingleton<MainMenuForm>();
            services.AddSingleton<App>();
            services.AddSingleton<AuthContainer>();
            services.AddSingleton<MainMenuContentControl>();
            services.AddSingleton<MenuControl>();
            services.AddSingleton<RulesOfGame>();
            services.AddSingleton<ProfileControl>();
            services.AddSingleton<GameRoomControl>();
            services.AddScoped<UserFormPlayerOne>();
            services.AddScoped<UserFormPlayerTwo>();
            services.AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(configuration);
            });
            ServiceProvider = services.BuildServiceProvider();

            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                context.Database.Migrate();
                if (!context.Cards.Any())
                {
                    var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
                    var ranks = Enumerable.Range(2, 13); // 2-14
                    foreach (var suit in suits)
                    {
                        foreach (var rank in ranks)
                        {
                            context.Cards.Add(new Card { Suit = suit, Rank = rank });
                        }
                    }
                    context.SaveChanges();
                }
                
            }

            
            ApplicationConfiguration.Initialize();
            var app = ServiceProvider.GetRequiredService<App>();
            app.Show();
            Application.Run();

          
           
        }
        
    }
}