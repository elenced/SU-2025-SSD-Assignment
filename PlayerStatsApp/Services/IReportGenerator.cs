using System.Collections.Generic;
using PlayerStatsApp.Models;




namespace PlayerStatsApp.Services
{
    /// <summary>
    /// Defines a contract for generating player reports
    /// Any class implementing this interface must provide functionality for creating a report from a list of players
    /// </summary>
    public interface IReportGenerator
    {
        /// <summary>
        /// Generates a report based on the provided list of players
        /// Implementations may choose how the report is formatted
        /// </summary>
        /// <param name="players">A list of Player objects to include in the report.</param>
        void GeneratePlayerReport(List<Player> players);
    }
}