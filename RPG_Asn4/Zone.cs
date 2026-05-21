namespace RPG_Asn4
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
        public Location CurrentLocation { get; set; }
        public Weather CurrentWeather {get; private set;}
        public TimeOfDay CurrentTime {get; private set;}
        private int turnsToTimeChange;
        public Zone(string name, Location startingLocation)
        {
            Name = name;
            CurrentLocation = startingLocation;
            CurrentWeather = Weather.Clear;
            CurrentTime = TimeOfDay.Morning;
            turnsToTimeChange = 4;  //takes 4 interaction turns to advance the time of day
        }

        public string? TickTurn(Player player)
        {
            string? environmentMessage = UpdateTimeAndWeather();
            //local is not a wide enough catch for what could be there.
            foreach(IInteractable local in CurrentLocation.Interactables)
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
            UI.Narrate($"{CurrentLocation.Name}");
            string? envMessage = TickTurn(player);  //bring the world alive
            if (!string.IsNullOrEmpty(envMessage))
            {
                UI.Narrate(envMessage);
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
            if(turnsToTimeChange <= 0)
            {
                //Advance time of day to the next phase, looping back to morning (0) after Night (3)
                CurrentTime = (TimeOfDay)(((int)CurrentTime + 1) % 4);
                turnsToTimeChange = 4;  //reset the counter for the next time phase

                if(Random.Shared.Next(100) < 40)
                {
                    Array weatherValues = Enum.GetValues(typeof(Weather));
                    Weather oldWeather = CurrentWeather;
                    CurrentWeather = (Weather)weatherValues.GetValue(Random.Shared.Next(weatherValues.Length))!;
                    if(oldWeather != CurrentWeather)
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
