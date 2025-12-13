using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerStatsApp.Models;

namespace PlayerStatsAppTests;

[TestClass]
public sealed class PlayerTests
{
    [TestMethod]
    public void UpdateStats_WithHigherScore_UpdatesHoursAndHighScore()
    {
        var player = new Player(id: 1, username: "Eve", hoursPlayed: 10.0, highScore: 100);

        player.UpdateStats(additionalHours: 5.0, newHighScore: 150);

        Assert.AreEqual(15.0, player.HoursPlayed, 0.001);
        Assert.AreEqual(150, player.HighScore);
    }

    [TestMethod]
    public void UpdateStats_WithLowerScore_DoesNotChangeHighScore()
    {
        var player = new Player(id: 1, username: "Eve", hoursPlayed: 10.0, highScore: 200);

        player.UpdateStats(additionalHours: 2.0, newHighScore: 150);

        Assert.AreEqual(12.0, player.HoursPlayed, 0.001);
        Assert.AreEqual(200, player.HighScore);
    }


    [TestMethod]
    public void UpdateStats_WithSameHighScore_DoesNotChangeHighScore()
    {
        var player = new Player(id: 1, username: "Eve", hoursPlayed: 10.0, highScore: 200);

        player.UpdateStats(additionalHours: 1.0, newHighScore: 200);

        Assert.AreEqual(11.0, player.HoursPlayed, 0.001);
        Assert.AreEqual(200, player.HighScore);
    }
}
