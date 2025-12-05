using System;
using System.IO;

namespace PlayerStatsApp.Services;

    /// <summary>
    /// Singleton logger class to log activity to a file.
    /// Only one instance of this class can exist.
    /// </summary>
    public class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();
        private readonly string _logFilePath = "activity_log.txt";

        private Logger() {}

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        public void Log(string message)
        {
            string timestamped = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            try
            {
                File.AppendAllText(_logFilePath, timestamped + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
