using System.Collections.Generic;


namespace PlayerStatsApp.Models; // creating a namespace to practice oop for better code organisation

public class Player
{
    public int Id { get; set; } 
    public string Username { get; set; } = string.Empty; // using string.empty to avoid null reference issues
    public double HoursPlayed { get; set; }
    public int HighScore { get; set; }

    public Player(int id, string username, double hoursPlayed, int highScore) //a constructor to initialise player object
    {
        Id = id;
        Username = username;
        HoursPlayed = hoursPlayed;
        HighScore = highScore;
    }
     
    public List<GameStats> GameStatistics { get; set; } = new List<GameStats>(); // initializing the list to avoid null reference issues

    public void UpdateStats(double additionalHours, int newHighScore)
    {
        HoursPlayed += additionalHours; // incrementing hours played
        if (newHighScore > HighScore) // only update high score if the new score is greater
        {
            HighScore = newHighScore; 
        }
    }

    public override string ToString() // makes player object easy to read when printed
    {
        return $"ID: {Id}, Username: {Username}, Hours Played: {HoursPlayed}, High Score: {HighScore}";
    }

}