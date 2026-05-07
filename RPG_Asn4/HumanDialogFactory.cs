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
