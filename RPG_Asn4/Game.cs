using System.Text.Json;

namespace RPG_Asn4
{

    //quick architecture styling 
    //Game owns:
    // - player
    // - current scene
    // - overall state

    // Scene owns:
    // - description
    // - list of interactable things in this location

    // InteractionHandler does:
    // - show list
    // - select target
    // - start interaction with selected target

    // Npc/Dialog system owns:
    // - talking to that selected NPC
    // - parser loop for typed verbs

    public class Game
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Exit,
        }
        
        public static Game? CurrentGame { get; private set; }
        public Scene? CurrentScene { get; private set; }
        //may need to start scaffolding Area/Room/County/City etc  Holodeck? plot device to be an area to generate a clue

        public ColorMode CurrentMode { get; set; } = ColorMode.FullColor;

        private GameState currentState;
        private const int StartingHealth = 20;  //start player with 20 health
        private Player? player;  //this is a nullable type, so it can be
        public bool isPlayerLoaded => player != null;
        private string path = AppDomain.CurrentDomain.BaseDirectory;  //this will put it in the same directory as executable, which is probably the bin/Debug/net10.0 folder
        public Game()
        {
            CurrentGame = this;
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
            Display.Igm("Thanks for playing!");
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

        public void PlayGame()
        {
            if (player == null)  //quick check for a player
            {
                Display.Error("You must create or load a character before you start.");
                currentState = GameState.MainMenu;
                return;
            }

            Display.Igm("--- Entering Game World ---");

            CurrentScene = CreateStartingScene(); //create the starting scene and describe it
            
            bool inWorld = true;
            while (inWorld)
            {
                inWorld = CurrentScene.Describe(player);
            }

            currentState = GameState.MainMenu;  //outside of inGame you are in the MainMenu
        }

        //CreateStartingScene returns a new Scene object, we may want to have this be more dynamic in the future, but for now it just creates a simple scene with a couple of NPCs in it.
        private Scene CreateStartingScene()
        {
            //make a list to hold the NPCs in the scene
            var startNpcs = new List<IInteractable>();

            //Add a couple of NPCs to the list
            for (int i = 0; i < 2; i++)
            {
                startNpcs.Add(HumanNpcFactory.GetStandardTier(1));
            }

            startNpcs.Add(AnimalNpcFactory.GetStandardTier(1));  //add an animal NPC to the starting scene for variety

            //Initialize the starting scene
            return new Scene(
                "You find yourself in a small clearing surrounded by dense forest.",
                startNpcs
            );
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

            Display.Igm($"{files.Length} available saved players: ");

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
                        Display.Igm($"Player loaded successfully");
                        Display.ShowPlayerInfo(player); //This shows the loaded player's information after successful loading.
                    }
                }
                catch (Exception e)
                {
                    Display.Error($"Error loading player data: {e.Message}");
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
