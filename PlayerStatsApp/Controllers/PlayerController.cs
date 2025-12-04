using PlayerStatsApp.Models; //importing the player class so it can be used here
using System.Collections.Generic; // importing the generic collections namespace to use lists


namespace PlayerStatsApp.Controllers // creating a namespace for controllers to manage different aspects of the application
{
    public class PlayerController
    {
        private List<Player> players = new List<Player>(); // creating a private list to store players, uses encapsulation

        public void LoadPlayers(List<Player> loadedPlayers) // method to load players from external source, takes a list of players as parameter
        {
            players = loadedPlayers ?? new List<Player>(); // assigns the loaded players to the private list, if null assigns an empty list
        }



        // add a new player
        public void AddPlayer(Player player) // adding a new player to the list, using void to perform an action without returning a value
        {
            players.Add(player);
        }

        // view all players
        public List<Player> GetAllPlayers() // using a method to view and return the player objects, used to view all players
        {
            return players; // returns the private list so the ui can display players
        }

        public Player? GetPlayerByUsername(string username)
        {
            return players.FirstOrDefault(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }


        public void UpdatePlayerStats(int id, double additonalHours, int newHighScore) // method to update player stats, takes player id, additional hours and new high score as parameters, using encapsulation to manage player data
        {
            var player = GetPlayerById(id); // calling the GetPlayerById method to find the player by their unique ID

            if (player != null) // checking if the player exists
            {
                player.UpdateStats(additonalHours, newHighScore); // calling the UpdateStats method from the Player model to update the stats
            }
        }

        // search player by ID
        public Player? GetPlayerById(int id) // using Player? method to return only one player based on their unique ID, returns null if not found, the parameter represtns the id of the player to returns, use ? in Player? to return null if the id is invalid
        {
            return players.FirstOrDefault(p => p.Id == id); // using a c# sharp method to seach for player by id, returns null if not found
        }
    } // explation of the equation: p refers to a player in the list, the lambda expression checks if the player's id matches the provided id
}