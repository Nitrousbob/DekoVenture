using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    public class AnimalDialogFactory
    {

        public static string GetRandomAnimalNoise()
        {
            string[] noises =
            {
                "A low growl that rolls through the darkness.",
                "A chittering from inside the walls of its voice box.",
                "A wet snarl.",
                "Claws scrape slowly across stone.",
                "A distant howl, then cuts off suddenly.",
                "breathes heavy.",
                "A sharp screech that tears through the silence.",
                "Tiny feet skitter across the floor.",
                "A beast lets out a broken, rasping cry.",
                "Wings flap overhead, but you see nothing.",
                "A deep hiss seeps from the shadows.",
                "Something gnaws loudly in the dark.",
                "A guttural bark answers from far away.",
                "A long, mournful wail drifts through the air.",
                "Something clicks its teeth together.",
                "A low purr vibrates from the blackness.",
                "A creature sniffs loudly, searching for you.",
                "A shrill cry echoes, almost like laughter.",
                "Something drags its claws across wood.",
                "A pack of unseen creatures yips in the distance.",
                "a hollow hoot.",
                "a rattling wheeze.",
                "a splashing watery sound.",
                "a raspy caw echoes between the trees.",
                "a soft whimper comes from somewhere nearby.",
                "a sudden snort breaks the silence.",
                "a clicking, insect-like trill.",
                "a deep croak that bubbles from the darkness.",
                "a snarls, then goes completely quiet.",
                "a strange animal cry that echoes like a warning."
                };

            int index = Random.Shared.Next(noises.Length);
            return noises[index];
        }


    public static void Dialogger(Animal a, Player p)
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
                            ComContext context = new ComContext(p, a);
                            action(ast, context);

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
