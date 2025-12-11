using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PlayerStatsApp.Models;

namespace PlayerStatsApp.Services
{
    public class FileController
    {
        private readonly string _filePath; // private class, using readonly for a safer set constructor

        public FileController(string filePath = "players.json") // constructor to store the JSON file
        {
            _filePath = filePath; // To save the value of the file path when an instance of the class is created
        }

        public List<Player> LoadPlayers() // method to return a list of players from the JSON file
        {
            if (!File.Exists(_filePath)) // checking if the file exists
            {
                return new List<Player>(); // if it doesnt exist, return an empty list so it doesn't crash
            }


            try // using a try block to catch potential errors during file reading and deserialization
            {
                string jsonString = File.ReadAllText(_filePath); // reading the entire content of the JSON file
                return JsonSerializer.Deserialize<List<Player>>(jsonString) ?? new List<Player>(); // comnverting the json text into the player list using the player model, uses a null-coalescing operator to return an empty list if deserialization fails
            }
            catch (JsonException ex) // using catch blocks to handle specific exceptions that may occur during file operations, it will print a message and return an empty list once again to avoid crashing, this one handles JSON format errors
            {
                Console.WriteLine($"Error loading players: {ex.Message}"); 
                return new List<Player>();
            }
            catch (IOException ex) // catches file-related errors such as missing file or access issues
            {
                Console.WriteLine ($"File access error while loading players: {ex.Message}");
                return new List<Player>();
            }
        }

        public void SavePlayers(List<Player> players) // method to save the list of players to the JSON file
        {
            try
            {
                var options = new JsonSerializerOptions // creating options for the JSON serializer to format the output nicely
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(players, options); // Converts Player list into JSON text
                File.WriteAllText(_filePath, jsonString); // writing the JSON text to the specified file, used for data persistence
        }
        catch (IOException ex)
     {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving players to file: {ex.Message}");
            Console.ResetColor();

            ActivityLog.GetInstance ().Log($"Error saving players to file");
            
            }
        }
    }
}