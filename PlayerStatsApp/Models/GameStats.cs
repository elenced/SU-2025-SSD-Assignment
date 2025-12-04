namespace PlayerStatsApp.Models
{
    public class GameStats
    {
        public string GameName { get; set; }    
        public int HighestScore { get; set;  }
        public double HoursPlayed { get; set; }

        public GameStats() {}


        public static List<string> GameList = new List<string>
        {
            "Rivals of Ether",
            "Cyber Quest",
            "Gate 3",
            "Crossing Paths",
            "Mystic Lands"
        };


        public GameStats(string gameName, int highestScore, double hoursPlayed)
        {
            GameName = gameName;
            HighestScore = highestScore;
            HoursPlayed = hoursPlayed;
        }
    }
}


