using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PlayerStatsApp.Models;

namespace PlayerStatsApp.Services
{
    public class FileController
    {
        private readonly string _filePath;

        public FileController(string filePath = "players.json")
        {
            _filePath = filePath;
        }

        public List<Player> LoadPlayers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Player>();
            }


            try
            {
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Player>>(jsonString) ?? new List<Player>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading players: {ex.Message}");
                return new List<Player>();
            }
            catch (IOException)
            {
                Console.WriteLine("File access error while loading players.");
                return new List<Player>();
            }
        }

        public void SavePlayers(List<Player> players)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(players, options);
                File.WriteAllText(_filePath, jsonString);
        }
        catch (IOException)
     {
            
            }
        }
    }
}