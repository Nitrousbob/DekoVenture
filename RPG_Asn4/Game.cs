using System.Text.Json;

namespace RPG_Asn4
{
    public class Game
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Exit,
        }
        
        private GameState currentState;
        private const int StartingHealth = 20;  //start player with 20 health
        private Player? player;  //this is a nullable type, so it can be
        public bool isPlayerLoaded => player != null;
        private string path = AppDomain.CurrentDomain.BaseDirectory;  //this will put it in the same directory as executable, which is probably the bin/Debug/net10.0 folder
        public Game()
        {
            currentState = GameState.MainMenu;
        }

        public void Run()
        {
            Display.ShowWelcomeMessage();
            while (currentState != GameState.Exit)
            {
                switch (currentState)
                {
                    case GameState.MainMenu:
                        ShowMainMenu();
                        break;
                    case GameState.Playing:
                    PlayGame();
                        break;
                }
            }
            Display.Igm("\nThanks for playing!");
        }

        private void ShowMainMenu()
        {
            Display.ShowMenuChoices(new string[] { "Create Character", "Load Character", "Start Game", "Exit" });
            switch (TakeInput.PromptIntRange("Please select an option: ", 1, 4))
            {
                case 1:
                    CreatePlayer();
                    break;
                case 2:
                    LoadPlayer();
                    break;
                case 3:
                    if (isPlayerLoaded)
                    {
                        currentState = GameState.Playing;
                    }
                    else
                    {
                        Display.Error("You must create or load a character before you start.");
                    }
                    break;
                case 4:
                    currentState = GameState.Exit;
                    break;
            }
        }

        private void PlayGame()
        {
            Display.Igm("\n--- Entering Game World ---");

            //make a couple Npcs to interact with in the starting area
            List<IInteractable> startNpcs = new List<IInteractable>
            {
                Npc.GetStandardTier1(),
                Npc.GetStandardTier1()
                //new Npc("Old Man", 10),
                //new Npc("Merchant", 5)
            };
            //Initialize the starting scene
            Scene startArea = new Scene(
                "\nYou find yourself in a small clearing surrounded by dense forest.",
                startNpcs
            );
            startArea.Describe();

            // For now, we return to the main menu after the scene is done.
            currentState = GameState.MainMenu;
            Display.Igm("\n--- Returning to the Main Menu ---");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
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

            int choice = TakeInput.PromptIntRange("Enter the number of the player you want to load: ", 1, files.Length);

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
