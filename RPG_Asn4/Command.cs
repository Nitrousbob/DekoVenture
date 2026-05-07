using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Asn4
{
    public class Command
    {
        Npc n;
        public void Look(List<Token> tokens, Enum type)
        {
            if (type == n.type.Humanoid)
            {
                Console.WriteLine($"You look at {n.Name}");
            }
        }

        public void Pet(List<Token> tokens)
        {
            Console.WriteLine("You pet the thing");
        }

        public void Help(List<Token> tokens)
        {
            Console.WriteLine("Available commands: ");
            Console.WriteLine("pet, look, help, exit, quit, bye");
        }

        public void Exit(List<Token> tokens)
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }
    }
}
