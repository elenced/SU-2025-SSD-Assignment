using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PlayerStatsApp.Models;


namespace PlayerStatsApp.Services
{

    /// <summary>
    /// Generates a summary report for players including average hours played, highest scorers, most active players
    /// and sorted rankings.
    /// Writes reports to a text file and logs key actions
    /// </summary>
    public class PlayerReport : IReportGenerator
    {
        private ActivityLog logger;
        private string reportPath = "player_report.txt";
        public PlayerReport()
        {
            logger = ActivityLog.GetInstance();
        }

        /// <summary>
        /// Generates a fromatted statistics report for all players
        /// Calculates averages, top performers and saves the report to a text file.
        /// </summary>
        /// <param name="players"></param>

        public void GeneratePlayerReport(List<Player> players)
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
                // Identify top scorer and most active player by iterating stats
                if (topScorer == null || p.HighScore > topScorer.HighScore)
                {
                    topScorer = p;
                }

                if (mostActive == null || p.HoursPlayed > mostActive.HoursPlayed)
                {
                    mostActive = p;
                }
            }
            // Caalculating overall averages for hours and high scores
            double averageHours = totalHours / players.Count;
            double averageHighScore = (double)totalHighScore / players.Count;


            // Sorts players by hours polayed in decreasing order using LINQ
            var playersByHours = players
                .OrderByDescending(p => p.HoursPlayed) // lambda that slects the property to sort by
                .ToList();

string summary = "─── ⋅ Player Statistics Summary ⋅ ───\n" +
                 $"Generated on: {DateTime.Now:F}\n" +
                 $"Total Players: {players.Count}\n" +
                 $"Average Hours Played: {averageHours:F2}\n" +
                 $"Average High Score: {averageHighScore:F2}\n" +
                 $"Top Scorer: {topScorer?.Username} with a score of {topScorer?.HighScore}\n" +
                 $"Most Active Player: {mostActive?.Username} with {mostActive?.HoursPlayed} hours played\n" +
                 "────────────────────────────────────\n" +
                 "Players by Hours Played:\n";

            foreach (var p in playersByHours)
            {
                summary +=  $"- {p.Username} | Hours: {p.HoursPlayed:F1} | High Score: {p.HighScore}\n";
            }


            Console.WriteLine(summary);
            try
            {
                // Saves the report to a text file
                File.WriteAllText(reportPath, summary);
                logger.Log("Player report saved to "  + reportPath);
            }
            catch (Exception ex)
            {
                logger.Log($"Error writing report, check here: {ex.Message}"); // Log any errros that occur while writing the file
            }
        }
    }
}