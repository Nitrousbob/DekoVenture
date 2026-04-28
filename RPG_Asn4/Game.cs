using System.Text.Json;

namespace RPG_Asn4
{
    public class Game
    {
        private const int StartingHealth = 20;
        private Player player = new("", StartingHealth);
        private string path = AppDomain.CurrentDomain.BaseDirectory;  //this will put it in the same directory as executable, which is probably the bin/Debug/net10.0 folder
        public Game()
        {

        }

        public void StartGame()
        {
            //make a couple Npcs to interact with in the starting area
            List<IInteractable> startNpcs = new List<IInteractable>
            {
                new Npc("Old Man", 10),
                new Npc("Merchant", 5)
            };

            //Initialize the starting scene
            Scene startArea = new Scene(
                "\nYou find yourself in a small clearing surrounded by dense forest.",
                startNpcs
            );

            startArea.Describe();
        }

        public void CreatePlayer()
        {
            string name = TakeInput.GetPlayerName();
            string fileName = Path.Combine(path, $"{name}.json");

            //check if a player with the same name already exists
            if (File.Exists(fileName))
            {
                Display.Igm($"Player `{name}` already exists. Loading instead...");
                LoadSpecificFile(fileName);
            }
            else
            {
                //Create and save
                player = new Player(name, StartingHealth);
                SavePlayer();
                Display.Igm("Player created and saved successfully.");
                Display.ShowPlayerInfo(player); //lets pass the player object to the display method to show the player's info after creation.
            }
        }

        public void LoadPlayer()
        {
            //Get files
            string[] files = Directory.GetFiles(path, "*.json").Where(file => !file.EndsWith(".deps.json") && !file.EndsWith(".runtimeconfig.json")).ToArray(); //This gets all the .json files in the directory, which should be the saved player files.
            if (files.Length == 0)  //If there are no .json files, it means there are no saved files to open
            {
                Display.Error("No saved player data found.");
                return;
            }

            Display.Igm($"\n{files.Length} available saved players: ");

            for (int i = 0; i < files.Length; i++)
            {

                Display.List($"{i + 1}. {Path.GetFileNameWithoutExtension(files[i])}"); //This lists the available saved player files by their names (without the .json extension).
            }

            int choice = TakeInput.PromptIntInstant("Enter the number of the player you want to load: ", Enumerable.Range(1, files.Length).ToArray());

                string selectedFile = files[choice - 1]; //This gets the file path of the selected player based on the user's input.

                try
                {
                    string jsonString = File.ReadAllText(selectedFile); //This reads the content of the selected file as a string.
                    Player? loadedPlayer = JsonSerializer.Deserialize<Player>(jsonString); //This attempts to deserialize the JSON string into a Player object.
                    if (loadedPlayer != null)
                    {
                        player = loadedPlayer;
                        Display.Igm($"\nPlayer loaded successfully");
                        Display.ShowPlayerInfo(player); //This shows the loaded player's information after successful loading.
                    }
                }
                catch (Exception e)
                {
                    Display.Error($"\nError loading player data: {e.Message}");
                }

        }

        private void LoadSpecificFile(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                Player? loaded = JsonSerializer.Deserialize<Player>(jsonString);
                if (loaded != null)
                {
                    player = loaded;
                    Display.Igm("Player loaded successfully.");
                    Display.ShowPlayerInfo(player);

                }
            }
            catch (Exception e)
            {
                Display.Error($"Error loading player data: {e.Message}");
            }
        }

        public void SavePlayer()
        {
            try
            {
                string fullPath = Path.Combine(path, $"{player.Name}.json");
                string jsonString = JsonSerializer.Serialize(player);
                File.WriteAllText(fullPath, jsonString);
            } catch (Exception e) 
            {
                Display.Error($"Save failed: {e.Message}");
            }
        }
    }
}
