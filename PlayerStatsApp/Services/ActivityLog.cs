using System;
using System.IO;

namespace PlayerStatsApp.Services
{
    /// <summary>
    /// A Singleton logger responsible for writing timestamped activity messages to a text file
    /// Ensures only one logging instance exists throughout the entire application
    /// </summary>
    public class ActivityLog
    {
        private static ActivityLog? _instance; // holds the single shared instance of the klogger
        private readonly string _logPath = "activity_log.txt"; // unified name

        private ActivityLog() { } // private constructor preverting external instantiation

        /// <summary>
        /// returns the single shared instance of the ActivityLog, creates a new instance only if one does not already exist
        /// </summary>


        public static ActivityLog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ActivityLog();
            }
            return _instance;
        }

        /// <summary>
        /// writes a timestamped message to the activity log file. if writing fails, an error is displayed to the console
        /// </summary>
        /// <param name="message">The message to record in the log file.</param>

        public void Log(string message)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}"; // format log entry with timestamp for clarity

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
