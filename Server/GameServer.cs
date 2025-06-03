using Microsoft.VisualBasic.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;


namespace PokerGameRSF.Server
{
    /// <summary>
    /// TCP-сервер, слушает клиентов.
    /// </summary>
    public class GameServer
    {
        private readonly TcpListener _listener;
        private readonly TcpClient _client = new();
        private readonly CancellationTokenSource _cts;
        private readonly string _logPath = "logs/server_log.txt";

        public event Action<GameMessage, TcpClient> OnMessageReceived;

        public GameServer(int port)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
            Directory.CreateDirectory("logs");
        }

        public void Start()
        {
            _listener.Start();
            //_ = AcceptClientsAsync;
            // TODO: что-то сломано

        }

        public void Stop()
        {
            _listener.Stop();
            _cts.Cancel();
        }

        public async Task AcceptClientsAsync(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                TcpClient client;
            }
        }
    }
}
