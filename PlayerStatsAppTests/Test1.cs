using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerStatsApp.Models;  // so we can use the Player class

namespace PlayerStatsAppTests;

[TestClass]
public sealed class PlayerTests
{
    [TestMethod]
    public void UpdateStats_WithHigherScore_UpdatesHoursAndHighScore()
    {
        // Arrange: create a player with starting stats
        var player = new Player(id: 1, username: "Eve", hoursPlayed: 10.0, highScore: 100);

        // Act: update with extra hours and a higher score
        player.UpdateStats(additionalHours: 5.0, newHighScore: 150);

        // Assert: hours increased, high score updated
        Assert.AreEqual(15.0, player.HoursPlayed, 0.001, "Hours not updated correctly");
        Assert.AreEqual(150, player.HighScore, "High score should update to new higher score");
    }

    [TestMethod]
    public void UpdateStats_WithLowerScore_DoesNotChangeHighScore()
    {
        // Arrange
        var player = new Player(id: 1, username: "Eve", hoursPlayed: 10.0, highScore: 200);

        // Act: new score is LOWER than existing
        player.UpdateStats(additionalHours: 2.0, newHighScore: 150);

        // Assert: hours increased, high score stayed the same
        Assert.AreEqual(12.0, player.HoursPlayed, 0.001, "Hours should still increase");
        Assert.AreEqual(200, player.HighScore, "High score should NOT go down");
    }
}
