using System;
using System.IO;

namespace PlayerStatsApp.Services
{
    public class ActivityLog
    {
        private static ActivityLog? _instance;
        private readonly string _logPath = "activity_log.txt"; // unified name

        private ActivityLog() { }

        public static ActivityLog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ActivityLog();
            }
            return _instance;
        }

        public void Log(string message)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";

            try
            {
                File.AppendAllText(_logPath, line);
            }
            catch (Exception ex)
            {
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Logging failed: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
