using System.Runtime.CompilerServices;

namespace DekoVenture
{
    public enum Weather
    {
        Clear,
        Cloudy,
        Raining,
        Storming,
        Foggy,
        Windy
    }

    public enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    public class Zone
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Overworld grid architecture to wander around the zone
        public Tile[,] MapGrid {get; private set;}
        public int PlayerX {get; set;}
        public int PlayerY {get; set;}

        //Tracks if the the player is inside a node map (cave, cabin, shop, etc)
        public Location? CurrentInterior {get; set;}
        
        //Compatibility Property
        //This ensures InteractionHandler and the rest of the game still works
        //If inside, it returns the room, if outside on the grid, it returns the tile's physical ground.
        public Location CurrentLocation
        {
            get
            {
                if(CurrentInterior != null) return CurrentInterior;

                var tile = MapGrid[PlayerX, PlayerY];
                return tile.EmptyGround;
            }
            set
            {
                //moving via exits sets the interior. Setting it to null puts them back on the overworld grid.
                CurrentInterior = value;
            }
        }

        public Weather CurrentWeather { get; private set; }
        public TimeOfDay CurrentTime { get; private set; }
        private int turnsToTimeChange;
        private readonly HashSet<Location> describedLocations = new();
        public Zone(string name, Location startingLocation)
        {
            Name = name;

            //Create a dummy 1x1 grid so the engine doesn't crash when checking tiles
            MapGrid = new Tile[1, 1];
            MapGrid[0,0] = new Tile(0,0, "Starting Area");
            PlayerX = 0;
            PlayerY = 0;

            //put the player inside their authored MUD-style node map
            CurrentInterior = startingLocation;

            CurrentWeather = Weather.Clear;
            CurrentTime = TimeOfDay.Morning;
            turnsToTimeChange = 8;  //takes 8 interaction turns to advance the time of day
        }

        //new Zelda - Style Grid Constructor
        public Zone(string name, int width, int height, int startX, int startY)
        {
            Name = name;
            MapGrid = new Tile[width, height];

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    MapGrid[x,y] = new Tile(x,y, "Wilderness");
                }
            }
            PlayerX = startX;
            PlayerY = startY;

            CurrentWeather = Weather.Clear;
            CurrentTime = TimeOfDay.Morning;
            turnsToTimeChange = 8;  //takes 8 interaction turns to advance the time of day
        }

        public string? TickTurn(Player player)
        {
            string? environmentMessage = UpdateTimeAndWeather();
            //local is not a wide enough catch for what could be there.
            foreach (IInteractable local in CurrentLocation.Interactables)
            {
                local.TickInteractionCooldown();

                if (local is Actor actor)
                {
                    actor.Vitals.TickEffects();
                    actor.StateMachine.Update();
                }
            }
            player.Vitals?.TickEffects();
            return environmentMessage;
        }

        public bool Describe(Player player)
        {
            UI.NarrateLocation($"You are at the <W>{CurrentLocation.Name}</W>, ");

            bool isFirstDescription = describedLocations.Add(CurrentLocation);
            if (!isFirstDescription)
            {
                string? envMessage = TickTurn(player);  //bring the world alive
                if (!string.IsNullOrEmpty(envMessage))
                {
                    UI.Narrate(envMessage);
                }
            }

            //check for player death during this part of turn tick
            if (player.Health <= 0)
            {
                return false;
            }
            return InteractionHandler.InteractWith(this, player);
        }

        private string? UpdateTimeAndWeather()
        {
            turnsToTimeChange--;
            if (turnsToTimeChange <= 0)
            {
                //Advance time of day to the next phase, looping back to morning (0) after Night (3)
                CurrentTime = (TimeOfDay)(((int)CurrentTime + 1) % 4);
                turnsToTimeChange = 4;  //reset the counter for the next time phase

                if (Random.Shared.Next(100) < 40)
                {
                    Array weatherValues = Enum.GetValues(typeof(Weather));
                    Weather oldWeather = CurrentWeather;
                    CurrentWeather = (Weather)weatherValues.GetValue(Random.Shared.Next(weatherValues.Length))!;
                    if (oldWeather != CurrentWeather)
                    {
                        return GetWeatherChangeMessage(); //exit early so we dont spam both a time and weather message at the exact same time.
                    }
                }
                return $"Time passes... it is now {CurrentTime.ToString().ToLower()}.";
            }
            return null;
        }

        private string GetWeatherChangeMessage()
        {
            return CurrentWeather switch
            {
                Weather.Clear => "The sky clears up and a pleasant breeze blows through.",
                Weather.Cloudy => "Dark clouds gather overhead, blocking out the light.",
                Weather.Raining => "A gentle rain begins to fall and the air is damp.",
                Weather.Storming => "Thunder crashes as a harsh storm begins!",
                Weather.Foggy => "A thick fog rolls in, making it hard to see.",
                Weather.Windy => "The wind howls through the trees, this is not hat weather.",
                _ => "The weather seems to change."
            };
        }
    }
}
