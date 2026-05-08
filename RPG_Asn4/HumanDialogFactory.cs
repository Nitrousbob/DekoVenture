namespace RPG_Asn4
{
public static class HumanDialogFactory
{
        
        public static string GetRandomGreeting(Npc n)
        {
        
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

        public static void Dialogger(Npc n, Player p)
        {
            bool talk = true;
            while (talk == true)
            {
                LookupTable lookupTable = new LookupTable();
                Console.WriteLine("What would you like to do?");
                var input = Console.ReadLine()?.ToLower();  //this can be migrated to TakeInput?
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("No command entered.");
                    continue;
                }

                if (input == "quit" || input == "exit" || input == "bye")
                {
                    talk = false;
                }
                else
                {
                    Tokenizer t = new Tokenizer();
                    var ast = t.Tokenize(input);

                    var verb = ast?.Where(x => x.Name == TokenType.verb).FirstOrDefault();
                    if (verb is not null)  //if no verb is found.
                    {
                        try
                        {
                            Action action = lookupTable[verb.Value];
                            action(ast);
                            
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("That verb is not recognized.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No verb found.");
                    }
                }
            }
        }

    }
}
