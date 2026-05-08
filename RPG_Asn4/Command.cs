using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RPG_Asn4
{
    //im starting to notice my Command class needs to know about the Context of the game for different commands.
    public class Command
    {
        public void Look(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is Npc n)
            {
                Display.Action($"You look at {n.Name}");
                string eyeBodyLanguage = HumanDialogFactory.NpcEyeBehavior(n);
                Display.Igm($"'{eyeBodyLanguage}'");
            }
            else
            {
                Console.WriteLine("There is nothing to look at.");
            }
        }

        public void Pet(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is Npc n)
            {
                Console.WriteLine($"You pet {n.Name}");
            }
            //if (c.CurrentTarget is Npc n)
            //{
            //    Display.Action($"You pet {n.Name}");
            //    string petResponse = HumanDialogFactory.NpcPetResponse(n);
            //    Display.Igm($"'{petResponse}'");
            //}
            //else

        }

        public void Help(List<Token> tokens, ComContext c)
        {
            Console.WriteLine("Available commands: ");
            Console.WriteLine("pet, look, help, exit, quit, bye");
        }

        public void Exit(List<Token> tokens, ComContext c)
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }
    }
}
