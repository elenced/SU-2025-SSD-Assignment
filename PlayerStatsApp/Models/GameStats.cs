using System.Collections.Generic;

namespace PlayerStatsApp.Models
{
    public class GameStats
    {
        public string GameName { get; set; }  = string.Empty;
        public int HighScore { get; set;  }
        public double HoursPlayed { get; set; }

        public GameStats() {} // creating an empty GameStats constructor for JSON deserialization 


        public static List<string> AvailableGames = new List<string> // creating a predefined list of games available for player selection
        {
            "Rivals of Ether",
            "Cyber Quest",
            "Gate 3",
            "Crossing Paths",
            "Mystic Lands"
        };


        public GameStats(string gameName, int highScore, double hoursPlayed) 
        {
            GameName = gameName;
            HighScore = highScore;
            HoursPlayed = hoursPlayed;
        }

        public override string ToString() // returns a readable string representation of the GameStats object
        {
            return $"Game: {GameName}, Highest Score: {HighScore}, Hours Played: {HoursPlayed}";
        }
    }
}


