using System;
using System.Collections.Generic;
using System.IO;
using PlayerStatsApp.Models;

namespace PlayerStatsApp.Services
{
    public class PlayerReport
    {
        private ActivityLog logger;
        private string reportPath = "player_report.txt";
        public PlayerReport()
        {
            logger = ActivityLog.GetInstance();
        }

        public void GenerateSummary(List<Player> players)
        {
            if (players.Count == 0)
            {
                logger.Log("Attempted to generate report with no players available.");
                Console.WriteLine("No players available to generate report, please try again.");
                return;
            }

            double totalHours = 0;
            int totalHighScore = 0;
            Player? topScorer = null;
            Player? mostActive = null;

            foreach (Player p in players)
            {
                totalHours += p.HoursPlayed;
                totalHighScore += p.HighScore;

                if (topScorer == null || p.HighScore > topScorer.HighScore)
                {
                    topScorer = p;
                }

                if (mostActive == null || p.HoursPlayed > mostActive.HoursPlayed)
                {
                    mostActive = p;
                }
            }
            double averageHours = totalHours / players.Count;
            double averageHighScore = (double)totalHighScore / players.Count;

            string summary = "─── ⋅ Player Statistics Summary ⋅ ───\n" +
                             $"Generated on: + {DateTime.Now:F}\n" +
                             $"Total Players: {players.Count}\n" +
                             $"Average Hours Played: {averageHours:F2}\n" +
                             $"Average High Score: {averageHighScore:F2}\n" +
                             $"Top Scorer: {topScorer?.Username} with a score of {topScorer?.HighScore}\n" +
                             $"Most Active Player: {mostActive?.Username} with {mostActive?.HoursPlayed} hours played\n";

            Console.WriteLine(summary);
            try
            {
                File.WriteAllText(reportPath, summary);
                logger.Log("Player report saved to "  + reportPath);
            }
            catch (IOException ex)
            {
                logger.Log($"Error writing report, check here: {ex.Message}");
            }
        }
    }
}