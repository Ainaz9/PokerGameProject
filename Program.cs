namespace PokerGamesRSF
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
            Application.Run(new Form1());
        }
    }

    public class Test
    {
        var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        // Configure DbContextFactory for PostgreSQL (connection string needed)
                        services.AddDbContextFactory<MyDbContext>(options =>
                            options.UseNpgsql("Your_Postgres_Connection_String_Here"));

                        services.AddScoped<IGameSessionService, GameSessionService>();
                        services.AddScoped<IUserService, UserService>();
                    })
                    .Build();

        // WinForms app would resolve services from host.Services
        // Example: var gameService = host.Services.GetRequiredService<IGameSessionService>();
        Console.WriteLine("Services configured. Run WinForms client to use game services.");
    }


}