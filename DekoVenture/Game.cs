using System.Text.Json;

namespace DekoVenture
{
    public class Game
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Exit,
        }
        
        public static Game? CurrentGame { get; private set; }
        public Zone? CurrentZone { get; private set; }
        //this colormode was for getting the game class working with the clr class
        public ColorMode CurrentMode { get; set; } = ColorMode.FullColor;
        private GameState currentState;
        private const int StartingHealth = 20;  //start player with 20 health
        private Player? player;  //this is a nullable type, so it can be
        public bool isPlayerLoaded => player != null;
        private string path = AppDomain.CurrentDomain.BaseDirectory;  //this will put it in the same directory as executable, which is probably the bin/Debug/net10.0 folder
        public StatusBar GameStatusBar { get; private set; }
        public Game()
        {
            CurrentGame = this;
            currentState = GameState.MainMenu;

            //initiate the status bar
            GameStatusBar = new StatusBar();
            GameStatusBar.AddComponent(new HealthComponent());
            GameStatusBar.AddComponent(new StatusEffectComponent());
            GameStatusBar.AddComponent(new CurrencyComponent());
        }

        public void Run()
        {
            UI.ShowWelcomeMessage();
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
            UI.Narrate("Thanks for playing!");
        }

        private void ShowMainMenu()
        {
            UI.ShowMenuChoices(new string[] { "Create Character", "Load Character", "Start Game", "Exit" });
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
                        UI.ShowError("You must create or load a character before you start.");
                    }
                    break;
                case 4:
                    currentState = GameState.Exit;
                    break;
            }
        }

        public void PlayGame()
        {
            if (player == null)  //quick check for a player
            {
                UI.ShowError("You must create or load a character before you start.");
                currentState = GameState.MainMenu;
                return;
            }

            UI.Narrate("--- Entering Game World ---");

            CurrentZone = WorldBuilder.CreateStartingZone(); //create the starting zone and describe it
            
            bool inWorld = true;
            while (inWorld)
            {
                GameStatusBar.Render(player);
                inWorld = CurrentZone.Describe(player);
                if (player.Health <= 0)
                {
                    UI.ShowError("\n ** You have died. Game Over. **");
                    inWorld = false;
                    player = null;
                }
            }
            
            currentState = GameState.MainMenu;  //outside of inGame you are in the MainMenu
        }
        
        public void CreatePlayer()
        {
            string name = TakeInput.GetPlayerName();
            string fileName = Path.Combine(path, $"{name}.json");

            //check if a player with the same name already exists
            if (File.Exists(fileName))
            {
                UI.Narrate($"Player `{name}` already exists. Loading instead...");
                LoadSpecificFile(fileName);
            }
            else
            {
                //Create and save
                player = new Player(name, StartingHealth);
                SavePlayer();
                UI.Narrate("Player created and saved successfully.");
                UI.ShowPlayerInfo(player); //lets pass the player object to the display method to show the player's info after creation.
            }
        }

        public void LoadPlayer()
        {
            //Get files
            string[] files = Directory.GetFiles(path, "*.json").Where(file => !file.EndsWith(".deps.json") && !file.EndsWith(".runtimeconfig.json")).ToArray(); //This gets all the .json files in the directory, which should be the saved player files.
            if (files.Length == 0)  //If there are no .json files, it means there are no saved files to open
            {
                UI.ShowError("No saved player data found.");
                return;
            }

            UI.Narrate($"\n{files.Length} available saved players: ");

            for (int i = 0; i < files.Length; i++)
            {

                UI.ShowListItem($"{i + 1}. {Path.GetFileNameWithoutExtension(files[i])}"); //This lists the available saved player files by their names (without the .json extension).
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
                        UI.Narrate($"Player loaded successfully");
                        UI.ShowPlayerInfo(player); //This shows the loaded player's information after successful loading.
                    }
                }
                catch (Exception e)
                {
                    UI.ShowError($"Error loading player data: {e.Message}");
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
                    UI.Narrate("Player loaded successfully.");
                    UI.ShowPlayerInfo(player);

                }
            }
            catch (Exception e)
            {
                UI.ShowError($"Error loading player data: {e.Message}");
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
                UI.ShowError($"Save failed: {e.Message}");
            }
        }
    }
}
