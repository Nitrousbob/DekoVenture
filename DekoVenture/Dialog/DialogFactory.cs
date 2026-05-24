namespace DekoVenture
{
    public static class DialogFactory
    {
        internal static readonly LookupTable lookupTable = new LookupTable();
        private static readonly Tokenizer tokenizer = new Tokenizer();

        public static string GetRandomGreeting(Npc n)
        {

            // 30% chance for the NPC to comment specifically on the current weather
            if (Random.Shared.Next(100) < 30 && Game.CurrentGame?.CurrentZone != null)
            {
                Weather currentWeather = Game.CurrentGame.CurrentZone.CurrentWeather;

                return currentWeather switch
                {
                    Weather.Clear => "Beautiful clear skies today, wouldn't you say?",
                    Weather.Cloudy => "Looks a bit gloomy with all these clouds. Hope it doesn't rain.",
                    Weather.Raining => "Miserable weather. I hate the rain.",
                    Weather.Storming => "By the gods, this storm is fierce! Stay safe out there.",
                    Weather.Foggy => "Can barely see my own hand in front of my face with this fog.",
                    Weather.Windy => "Hold onto your hat! This wind is something fierce.",
                    _ => "Strange weather we're having."
                };
            }

            string[] NpcTownsfolkGreeting =
                {
                "Lovely weather we're having, isn't it?",
                "Have you heard the latest gossip from the market?",
                "I remember when this town was just a small village.",
                "The harvest has been bountiful this year.",
                "Be careful out there, the woods can be dangerous.",
                "I heard there's a new shop opening up soon.",
                "The old mill is said to be haunted at night.",
                "Have you tried the baker's new pastry? It's delicious!",
                "The festival is coming up, it's always a good time.",
                "I wish I could travel, but I'm too old for that now."
            };

            int index = Random.Shared.Next(NpcTownsfolkGreeting.Length);
            return NpcTownsfolkGreeting[index];
        }

        public static string NpcEyeBehavior(Npc n)
        {
            if (n.HasEyes)
            {
                string[] looking = {
                "Their eyes flick toward you, then quickly away.",
                "They hold steady eye contact as they speak.",
                "Their eyes narrow slightly, studying your face.",
                "They glance over your shoulder, as if distracted.",
                "Their eyes brighten with sudden interest.",
                "They blink slowly, choosing their words carefully.",
                "Their gaze drops to the ground for a moment.",
                "Their eyes dart around the room nervously.",
                "They squint at you with quiet suspicion.",
                "Their eyes soften as they listen."
            };

                int index = Random.Shared.Next(looking.Length);
                return looking[index];
            }
            else
            {
                string[] noEyes = {
                "They stare blankly at you, unable to make eye contact.",
                "Without eyes, they seem to sense your presence in other ways.",
                "They gesture with their hands instead of making eye contact.",
                "Their head tilts slightly, as if trying to understand you.",
                "They seem to listen intently, despite lacking eyes.",
                "They emit a soft hum, responding to your words.",
                "Their body language is expressive, compensating for no eyes.",
                "They seem to feel the air around them, sensing your emotions.",
                "They nod slowly, acknowledging your presence without sight.",
                "Despite having no eyes, they seem fully engaged in the conversation."
            };
                int index = Random.Shared.Next(noEyes.Length);
                return noEyes[index];
            }
        }

        public static bool HandleDialogTurn(Actor a, Player p)
        {
            var input = TakeInput.GetString("What would you like to do?")?.ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                UI.ShowError("No command entered.");
                return true;
            }

            if (input == "quit" || input == "exit" || input == "bye")
            {
                return false;
            }

            var ast = tokenizer.Tokenize(input);

            if (ast != null)
            {
                foreach (Token token in ast)
                {
                    UI.ShowListNumber($"{token.Name}: {token.Value}");
                }
            }

            var verb = ast?.FirstOrDefault(x => x.Name == TokenType.verb);
            if (verb is not null)
            {
                try
                {
                    Action action = lookupTable[verb.Value];
                    ComContext context = new ComContext(p, a);
                    action(ast, context);

                    if (context.EndInteration || !a.canInteract || !a.IsAlive || !p.IsAlive)
                    {
                        return false;
                    }
                }
                catch (KeyNotFoundException)
                {
                    UI.ShowError("That verb is not recognized.");
                }
            }
            else
            {
                UI.ShowError("No verb found.");
            }

            return true;
        }

    }
}
