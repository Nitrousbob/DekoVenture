using System.Text.Json;

namespace RPG_Asn4
{
    public class Game
    {
        private const int StartingHealth = 20;
        Player player = new Player("", StartingHealth);
        
        string path = AppDomain.CurrentDomain.BaseDirectory;  //this will put it in the same directory as executable, which is probably the bin/Debug/net10.0 folder

        public Game()
        {

        }

        public void CreatePlayer()
        {
            Console.WriteLine("Creating a new character...");
            string name = TakeInput.GetPlayerName();
            player = new Player(name, StartingHealth); //players start out with 20 health by default.
            SavePlayer();
        }

        public void LoadPlayer()
        {
            //i had an issue where the .json files were being created in the bin/Debug/net10.0 folder,
            //but when i tried to load them, it couldn't find them because it was looking in the project
            //folder. So now I'm using AppDomain.CurrentDomain.BaseDirectory to get the correct path to
            //the .json files.

            string[] files = Directory.GetFiles(path, "*.json").Where(file => !file.EndsWith(".deps.json") && !file.EndsWith(".runtimeconfig.json")).ToArray(); //This gets all the .json files in the directory, which should be the saved player files.
            if (files.Length == 0)  //If there are no .json files, it means there are no saved files to open
            {
                Console.WriteLine("No saved player data found. Please create a character first.");
                return;
            }

            Console.WriteLine($"{files.Length} available saved players: ");

            for (int i = 0; i < files.Length; i++)
            {

                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(files[i])}"); //This lists the available saved player files by their names (without the .json extension).
            }

            Console.Write("Enter the number of the player you want to load: ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= files.Length)
            {
                string selectedFile = files[choice - 1]; //This gets the file path of the selected player based on the user's input.

                try
                {
                    string jsonString = File.ReadAllText(selectedFile); //This reads the content of the selected file as a string.
                    Player? loadedPlayer = JsonSerializer.Deserialize<Player>(jsonString); //This attempts to deserialize the JSON string into a Player object.
                    if (loadedPlayer != null)
                    {
                        player = loadedPlayer;
                        Display.Igm($"Player loaded successfully. Name: {player.Name}, Health: {player.Health}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading player data: {e.Message}");
                }

            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                return;
            }
        }


        public void SavePlayer()
        {
            if (!File.Exists(Path.Combine(path, $"{player.Name}.json")))
            {
                using StreamWriter sw = File.CreateText(Path.Combine(path, $"{player.Name}.json"));
                {
                    sw.WriteLine(JsonSerializer.Serialize(player));
                    Display.Igm($"Player {player.Name} saved successfully with {player.Health} health.");
                }

            }

        }
    }
}
