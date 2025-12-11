using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerStatsApp.Controllers;
using PlayerStatsApp.Models;
using System.Collections.Generic;

namespace PlayerStatsAppTests;

[TestClass]
public sealed class PlayerControllerTests
{
    private PlayerController controller;

    [TestInitialize]
    public void SetUp()
    {
        controller = new PlayerController();

        var players = new List<Player>
        {
            new Player(1, "Zara", 10.0, 100),
            new Player(2, "James",   20.0, 300),
            new Player(3, "Eve",    5.0, 150)
        };

        controller.LoadPlayers(players);
    }

    [TestMethod]
    public void GetPlayerById_ValidId_ReturnsCorrectPlayer()
    {
        var result = controller.GetPlayerById(2);

        Assert.IsNotNull(result);
        Assert.AreEqual("James", result!.Username);
    }

    [TestMethod]
    public void GetPlayerById_InvalidId_ReturnsNull()
    {
        var result = controller.GetPlayerById(999);

        Assert.IsNull(result, "Non-existent ID should return null");
    }

    [TestMethod]
    public void LinearSearchByUsername_CaseInsensitiveMatch_ReturnsPlayer()
    {
        var result = controller.LinearSearchByUsername("EVE");

        Assert.IsNotNull(result);
        Assert.AreEqual("Eve", result!.Username);
    }

    [TestMethod]
    public void LinearSearchByUsername_UnknownUser_ReturnsNull()
    {
        var result = controller.LinearSearchByUsername("NotARealUser");

        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetPlayersSortedByHighScoreManual_ReturnsPlayersInDescendingOrder()
    {
        var sorted = controller.GetPlayersSortedByHighScoreManual();

        Assert.AreEqual(3, sorted.Count);
        Assert.AreEqual("James",   sorted[0].Username); 
        Assert.AreEqual("Eve",   sorted[1].Username); 
        Assert.AreEqual("Zara", sorted[2].Username); 

        Assert.IsTrue(sorted[0].HighScore >= sorted[1].HighScore);
        Assert.IsTrue(sorted[1].HighScore >= sorted[2].HighScore);
    }
}
