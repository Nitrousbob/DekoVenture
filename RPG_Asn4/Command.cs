using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RPG_Asn4
{
    public class Command
    {
        public void Look(List<Token> tokens, Npc n)
        {
            Console.WriteLine($"You look at {n.Name}");
            string eyeBodyLanguage = HumanDialogFactory.NpcEyeBehavior(n);
            Display.Igm($"\n'{eyeBodyLanguage}'");
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
