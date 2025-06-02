using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameRSF.Models
{
    public class Logger
    {
        private string _logFilePath;

        public Logger(string logFilePath = "log.txt")
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Ошибка логирования: {exception.Message}");
            }
        }
    }
}
