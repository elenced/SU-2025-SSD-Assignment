using System;
using PlayerStatsApp.Controllers;
using PlayerStatsApp.Models;
using PlayerStatsApp.Services;

FileController fileController = new FileController("players.json"); // creating an instance of the FileController to handle file operations
PlayerController playerManager = new PlayerController(); // manages player in memory

var existingPlayers = fileController.LoadPlayers(); // loading existing players from the file using the file controller
playerManager.LoadPlayers(existingPlayers); // loading the existing players into the player manager


int nextPlayerID = existingPlayers.Count + 1; // initializing a variable to assign unique IDs to new players, so each player can be identified distinctly
bool running = true; // creating a boolean to control the menu loop


while (running)
{
    Console.ForegroundColor = ConsoleColor.Magenta; // setting the console text color to cyan for aethetics

    Console.WriteLine("─── ⋅ Player Statistics Manager ⋅ ───");
    Console.WriteLine("1. Add New Player");
    Console.WriteLine("2. View Players");
    Console.WriteLine("3. Update Player Stats");
    Console.WriteLine("4. Search Player by ID");
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

            fileController.SavePlayers(playerManager.GetAllPlayers()); // saving all players to the file after adding a new player, ensuring data persistence

            nextPlayerID++; // incrementing the player ID for the next new player
            Console.WriteLine("[■■■■■■■■■] 100%! Player added successfully!");
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"ID: {player.Id} | Username: {player.Username}");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine ($"Hours Played: {player.HoursPlayed} | High Score: {player.HighScore}");
                }
            }
            break;
        case "3":
            Console.Clear();
            Console.WriteLine("─── ⋅ Update Player Stats ⋅ ───");
            Console.Write("Enter Player ID to update: ");
            string updateIdInput = Console.ReadLine();

            if (!int.TryParse(updateIdInput, out int updateId)) // using inttryparse to convert string into integer and handle invalid input effectively, prevents crashing + allows for easy testing
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID. Returning to menu.");
                Console.ResetColor();
                break;
            }

           
            Player? playerToUpdate = playerManager.GetPlayerById(updateId); // searching for the player by ID using the player manager, uses Player? as it may return null, displaying search algorithm on data
            if (playerToUpdate == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Player not found! Please re-try.");
                Console.ResetColor();
                break;
            }
            
            Console.WriteLine($"Current stats for {playerToUpdate.Username}: Hours Played - {playerToUpdate.HoursPlayed}, High Score - {playerToUpdate.HighScore}");

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

            if (!int.TryParse(updateScoreInput, out int newHighScore)) // using the out keyword to store the parsed integer value directly into newHighScore variable, using ! to check for invalid input, shows error is so
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid score input. Stats not updated, please try again");
                Console.ResetColor();
                break;
            }

            playerManager.UpdatePlayerStats(updateId, additionalHours, newHighScore); // updating player stats using the controller
            fileController.SavePlayers(playerManager.GetAllPlayers()); // saving all players to the file after updating stats, ensuring data persistence

            playerToUpdate.UpdateStats(additionalHours, newHighScore); // calls player model to add/update the stats, using encapsulation as the ui doesnst change properties directly
            Console.WriteLine("Player stats updated successfully!");
            break;
        case "4":
            Console.WriteLine("─── ⋅ Search for a Player by ID ⋅ ───");

            Console.Write("Enter Player ID to search: ");
            string iDInput = Console.ReadLine();
            if (!int.TryParse(iDInput, out int searchId)) // using inttryparse to convert string into integer and handle invalid input effectively, prevents crashing + allows for easy testing
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error, please enter a numerical value. Returning to menu.");
                Console.ResetColor();
                break;
            }
            
            var foundPlayer = playerManager.GetPlayerById(searchId); //asking the controller to get the player by ID

            
            if (foundPlayer != null)
            {
                Console.WriteLine("Player found:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {foundPlayer.Id} | Username: {foundPlayer.Username}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine ($"Hours Played: {foundPlayer.HoursPlayed} | High Score: {foundPlayer.HighScore}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID! Please re-try.");
                Console.ResetColor();
            }

            break;
        case "5":


            Console.WriteLine("Generating report...");
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