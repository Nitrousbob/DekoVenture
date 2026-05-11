namespace RPG_Asn4
{
    //im starting to notice my Command class needs to know about the Context of the game for different commands.
    public class Command
    {
        public void Look(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is Animal a)
            {
                Display.Action(a.GetDescription());
                //string eyeBodyLanguage = AnimalDialogFactory.NpcEyeBehavior(a);
                //Display.Igm($"'{eyeBodyLanguage}'");
            }
            else if (c.CurrentTarget is Npc n)
            {
                Display.Action(n.GetDescription());
                string eyeBodyLanguage = HumanDialogFactory.NpcEyeBehavior(n);
                Display.Igm($"'{eyeBodyLanguage}'");
            }
            else
            {
                Console.WriteLine("There is nothing to look at.");
            }
        }
        public void Talk(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is ITalkable talkable)
            {
                Display.Action($"You talk to {talkable.Name}");
                Display.Igm(talkable.GetTalkResponse());
            }
            else
                {
                    Display.Igm($"{c.CurrentTarget.Name} doesn't seem to be able to talk.");
                }
        }

        public void Ask(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IQuestionable questionable)
            {
                Display.Action($"You ask {questionable.Name} a question.");
                Display.Igm(questionable.GetQuestionResponse());
            }
            else
            {
                Display.Igm($"{c.CurrentTarget} doesn't seem to be able to answer questions.");
            }
        }


        public void Pet(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IPettable pettable)
            {
                Display.Action($"You pet {pettable.Name}");
                Display.Igm(pettable.GetPetResponse());
            }
            else if (c.CurrentTarget is Npc n)
            {
                Display.Action($"You try to pet {n.Name}");
                Display.Igm($"{n.Name} doesn't seem to like that.");
            }
            else
            {
                Display.Igm("Very unusual, that's not something you can pet.");
            }

        }

        public void Slap(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IInteractable interactable)
            {
                Display.Action($"You slap {interactable.Name}");
                Display.DarkAction($"{interactable.Name} looks at you with shock and anger.");
                interactable.BlockInteraction(3);  //mad for 3 turns, you can't interact with them for 3 turns.
                //remove interactable from npc while they are slapped, then add them back after a few turns to simulate them being mad at you for a while.
                //this was put into the Animals and Npcs OnInteract method, so they will stop interacting with you for a few turns after being slapped.
            }
            else
            {
                Console.WriteLine("That didn't just happen again!");
            }
        }

        public void Help(List<Token> tokens, ComContext c)
        {
            Display.Action("Available commands: ");
            Display.Bright("pet, look, help, exit, quit, bye, hit, slap, talk");
            //can the list for actions build up from a dictionary and display available options?
        }

        public void Exit(List<Token> tokens, ComContext c)
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }
    }
}
