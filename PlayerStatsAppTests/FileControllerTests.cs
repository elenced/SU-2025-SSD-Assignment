using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerStatsApp.Models;
using PlayerStatsApp.Services;
using System.Collections.Generic;
using System.IO;

namespace PlayerStatsAppTests;

[TestClass]
public sealed class FileControllerTests
{
    private readonly string testFilePath = "test_players.json";

    [TestCleanup]
    public void CleanUp()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [TestMethod]
    public void LoadPlayers_MissingFile_ReturnsEmptyList()
    {
        // Arrange: ensure the test file does NOT exist
        if (File.Exists(testFilePath))
            File.Delete(testFilePath);

        var fileController = new FileController(testFilePath);

        // Act
        var players = fileController.LoadPlayers();

        // Assert
        Assert.IsNotNull(players);
        Assert.AreEqual(0, players.Count, "Missing file should return an empty list");
    }

    [TestMethod]
    public void SavePlayers_ThenLoadPlayers_RoundTripsDataCorrectly()
    {
        // Arrange
        var originalPlayers = new List<Player>
        {
            new Player(1, "Alice", 10.0, 100),
            new Player(2, "Bob",   20.0, 200)
        };

        var fileController = new FileController(testFilePath);

        // Act
        fileController.SavePlayers(originalPlayers);
        var loadedPlayers = fileController.LoadPlayers();

        // Assert
        Assert.AreEqual(originalPlayers.Count, loadedPlayers.Count);
        Assert.AreEqual("Alice", loadedPlayers[0].Username);
        Assert.AreEqual("Bob",   loadedPlayers[1].Username);
    }
}
