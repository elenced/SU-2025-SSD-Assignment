using System;
using PlayerStatsApp.Controllers;
using PlayerStatsApp.Models;
using PlayerStatsApp.Services;
using System.Linq;
using System.Security;
ActivityLog logger = ActivityLog.GetInstance(); // initializing the singleton logger instance to log application activities

FileController fileController = new FileController("players.json"); // creating an instance of the FileController to handle file operations
PlayerController playerManager = new PlayerController(); // manages player in memory
PlayerReport reportGenerator = new PlayerReport(); // creating an instance of the PlayerReport to handle report generation


var existingPlayers = fileController.LoadPlayers(); // loading existing players from the file using the file controller
playerManager.LoadPlayers(existingPlayers); // loading the existing players into the player manager


int nextPlayerID = existingPlayers.Count + 1; // initializing a variable to assign unique IDs to new players, so each player can be identified distinctly
bool running = true; // creating a boolean to control the menu loop


while (running)
{
    Console.ForegroundColor = ConsoleColor.Magenta; // setting the console text color to cyan for aethetics
    logger.Log("Console app loaded.");
    Console.WriteLine("─── ⋅ Player Statistics Manager ⋅ ───");
    Console.WriteLine("1. Add New Player");
    Console.WriteLine("2. View Players");
    Console.WriteLine("3. Update Player Stats");
    Console.WriteLine("4. Search Player by ID or Username");
    Console.WriteLine("5. Generate Report");
    Console.WriteLine("6. Leave Application");
    Console.Write("Select an option (1-6): ");
    Console.ResetColor();

    string choice = Console.ReadLine(); // number gets stored into the choice variable

    switch (choice) // using the switch statment for the menu, avoiding the complexity of multiple if-else statements
    {
        case "1":
            Console.Clear(); // clearing the console for better readability
            Console.WriteLine("─── ⋅ Add a new player! ⋅ ───");
            Console.Write("Enter player Username: ");
            string username = Console.ReadLine(); // reading the username input from the user

            Console.WriteLine("Games to add to your library:");
            for (int i = 0; i < GameStats.AvailableGames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GameStats.AvailableGames[i]}");
            }

            Console.Write("Select a game (1-" + GameStats.AvailableGames.Count + "):");
            string gameChoiceInput = Console.ReadLine();

            if (!int.TryParse(gameChoiceInput, out int gameChoice) || gameChoice < 1 || gameChoice > GameStats.AvailableGames.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid game selection. Player not added, please try again.");
                Console.ResetColor();
                break;
            }
            string selectedGame = GameStats.AvailableGames[gameChoice - 1];

            Console.Write("Enter hours played: ");
            string hoursInput = Console.ReadLine();

            if (!double.TryParse(hoursInput, out double hoursPlayed))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid hours input. Player not added.");
                Console.ResetColor();
                break;
            }

            Console.Write("Enter high score: ");
            string scoreInput = Console.ReadLine();

            if (!int.TryParse(scoreInput, out int highScore))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid score input. Player not added.");
                Console.ResetColor();
                break;
            }

            Player newPlayer = new Player(nextPlayerID, username, hoursPlayed, highScore); // creating a new player object with the provided details, using model layer to represent the player

            playerManager.AddPlayer(newPlayer); // adding the new player to the player manager, using controller layer to handle the addition
           
            var gameStat = new GameStats(selectedGame, highScore, hoursPlayed); 
            newPlayer.GameStatistics.Add(gameStat); // adding the game statistics to the player's game statistics list


            fileController.SavePlayers(playerManager.GetAllPlayers()); // saving all players to the file after adding a new player, ensuring data persistence

    
            nextPlayerID++; // incrementing the player ID for the next new player
            Console.WriteLine("[■■■■■■■■■] 100%! Player added successfully!");
            logger.Log($"New player added: {username} (ID: {nextPlayerID - 1}).");
            break;


        case "2":
            Console.Clear();
            Console.WriteLine("─── ⋅ View all  players! ⋅ ───");

            var players = playerManager.GetAllPlayers(); // retrieving all players from the player Manager

            if (players.Count == 0)
            {
                Console.WriteLine("No players could be found, please add a player first.");
            }
            else
            {
                foreach (var player in players) // iterating through each player and displaying their details
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("────────────────────────────────────────────────────────────────────");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"ID: {player.Id} | Username: {player.Username}");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine ($"Hours Played: {player.HoursPlayed} | High Score: {player.HighScore}");

                    if (player.GameStatistics != null && player.GameStatistics.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Games:");

                        foreach (var stat in player.GameStatistics)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine($"     - {stat.GameName}: → Hours Played - {stat.HoursPlayed} | High Score - {stat.HighScore}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("   No game specific stats recorded for this player.");
                    }
                }
            }
            break;
  case "3":
{
    Console.Clear();
    Console.WriteLine("─── ⋅ Update Player Stats ⋅ ───");
    Console.Write("Enter Player ID to update: ");
    string updateIdInput = Console.ReadLine();

    if (!int.TryParse(updateIdInput, out int updateId))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid ID. Returning to menu.");
        Console.ResetColor();
        break;
    }

    Player? playerToUpdate = playerManager.GetPlayerById(updateId);
    if (playerToUpdate == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Player not found! Please re-try.");
        Console.ResetColor();
        break;
    }

    Console.WriteLine(
        $"Current stats for {playerToUpdate.Username}: " +
        $"Hours Played - {playerToUpdate.HoursPlayed}, High Score - {playerToUpdate.HighScore}");

    if (playerToUpdate.GameStatistics == null)
    {
        playerToUpdate.GameStatistics = new List<GameStats>();
    }


    GameStats selectedGameStats;

    Console.WriteLine("Update either existing game stats or add new game stats.");
    Console.WriteLine("1. Update Existing Game Stats");
    Console.WriteLine("2. Add New Game Stats");
    Console.Write("Select an option (1-2): ");
    string updateOption = Console.ReadLine();

 if (updateOption == "2")
{

    while (true)
    {
        Console.WriteLine("Choose a game to add stats for:");
        for (int i = 0; i < GameStats.AvailableGames.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {GameStats.AvailableGames[i]}");
        }

        Console.Write($"Select a game (1-{GameStats.AvailableGames.Count}, or 0 to cancel): ");
        string newGameChoice = Console.ReadLine();

        
        if (newGameChoice == "0")
        {
            Console.WriteLine("Cancelled adding new game stats. Returning to main menu.");
            return; // or 'break;' if you want to just exit case "3"
        }

        if (!int.TryParse(newGameChoice, out int newGameIndex) ||
            newGameIndex < 1 || newGameIndex > GameStats.AvailableGames.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid game selection. Please try again.");
            Console.ResetColor();
            continue; // repeat the loop
        }

        string newGameName = GameStats.AvailableGames[newGameIndex - 1];

       
        var existingGame = playerToUpdate.GameStatistics
            .FirstOrDefault(gs => gs.GameName.Equals(newGameName, StringComparison.OrdinalIgnoreCase));

        if (existingGame != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This player already has stats for that game.");
            Console.WriteLine("Please choose a different game.");
            Console.ResetColor();
            continue; // repeat the loop and show the list again
        }

        
        selectedGameStats = new GameStats(newGameName, 0, 0);
        playerToUpdate.GameStatistics.Add(selectedGameStats);
        break; // exit the while loop and continue with asking for hours / score
    }
}
    
    else if (updateOption == "1")
    {
        if (playerToUpdate.GameStatistics.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Player has no game stats yet. Add a new game first.");
            Console.ResetColor();
            break;
        }

        Console.WriteLine("Select a game to update stats for:");
        for (int i = 0; i < playerToUpdate.GameStatistics.Count; i++)
        {
            var gs = playerToUpdate.GameStatistics[i];
            Console.WriteLine($"{i + 1}. {gs.GameName} " +
                              $"(Hours Played: {gs.HoursPlayed}, High Score: {gs.HighScore})");
        }

        Console.Write("Enter selection: ");
        string gameIndexInput = Console.ReadLine();

        if (!int.TryParse(gameIndexInput, out int gameIndex) ||
            gameIndex < 1 || gameIndex > playerToUpdate.GameStatistics.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection. Stats not updated, please try again.");
            Console.ResetColor();
            break;
        }

        selectedGameStats = playerToUpdate.GameStatistics[gameIndex - 1];
    }
    
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid option. Stats not updated, please try again.");
        Console.ResetColor();
        break;
    }

    
    Console.Write("Enter additional hours played: ");
    string updateHoursInput = Console.ReadLine();

    if (!double.TryParse(updateHoursInput, out double additionalHours))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid hours input. Stats not updated, please try again.");
        Console.ResetColor();
        break;
    }

    Console.Write("Enter new high score: ");
    string updateScoreInput = Console.ReadLine();

    if (!int.TryParse(updateScoreInput, out int newHighScore))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid score input. Stats not updated, please try again");
        Console.ResetColor();
        break;
    }

    selectedGameStats.HoursPlayed += additionalHours;

    if (newHighScore > selectedGameStats.HighScore)
    {
        selectedGameStats.HighScore = newHighScore;
    }
    else
    {
        Console.WriteLine("New high score is not greater than existing high score. High score not updated.");
    }

    // recompute overall totals from per-game stats
    playerToUpdate.HoursPlayed = playerToUpdate.GameStatistics.Sum(gs => gs.HoursPlayed);
    playerToUpdate.HighScore = playerToUpdate.GameStatistics.Max(gs => gs.HighScore);

    fileController.SavePlayers(playerManager.GetAllPlayers());
    Console.WriteLine("Player stats updated successfully!");
    logger.Log($"Stats updated for player ID {playerToUpdate.Id}, game: {selectedGameStats.GameName}.");
    break;
}
        case "4":
            Console.WriteLine("─── ⋅ Search for a Player by ID or Username ⋅ ───");

            Console.Write("Enter Player ID  or Username to search: ");
            string searchInput = Console.ReadLine();
            Player? foundPlayer;
            if (int.TryParse(searchInput, out int searchId)) // using inttryparse to convert string into integer and handle invalid input effectively, prevents crashing + allows for easy testing
            {
                foundPlayer = playerManager.GetPlayerById(searchId); //asking the controller to get the player by ID
            }
            else
            {
             foundPlayer = playerManager.GetPlayerByUsername(searchInput); // asking the controller to get the player by username
            }
            
        
            if (foundPlayer != null)
            {
                Console.WriteLine("Player found:");
                logger.Log($"Search successful for player: {foundPlayer.Username} (ID: {foundPlayer.Id}).");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {foundPlayer.Id} | Username: {foundPlayer.Username}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine ($"Hours Played: {foundPlayer.HoursPlayed} | High Score: {foundPlayer.HighScore}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID or Username! Please re-try.");
                logger.Log($"Search failed. No player found for input: '{searchInput}'.");
                Console.ResetColor();
            }

            break;
        case "5":
            Console.Clear();
            Console.WriteLine("─── ⋅ Generate Player Report ⋅ ───");
            var allPlayerForReport = playerManager.GetAllPlayers(); // retrieving all players from the player manager for report generation

            if (allPlayerForReport.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No players available to generate report, please add players first.");
                Console.ResetColor();
            }
            
            {
                reportGenerator.GenerateSummary(allPlayerForReport);  // generating the player report using the PlayerReport service
                logger.Log("Player report generated successfully.");
            }

            break;
        case "6":
            // Exit the application
            fileController.SavePlayers(playerManager.GetAllPlayers()); // saving all players to the file before exiting, using file controller to handle file operations
            running = false;
            Console.WriteLine("Saving data and exiting application. Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid input, try again.");
            break;
    }
    Console.WriteLine(); 
}