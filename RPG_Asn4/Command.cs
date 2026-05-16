﻿namespace RPG_Asn4
{
    //im starting to notice my Command class needs to know about the Context of the game for different commands.
    public class Command
    {
        public void Look(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is Animal a)
            {
                Display.Action(a.GetDescription());
            }
            else if (c.CurrentTarget is Npc n)
            {
                Display.Action(n.GetDescription());
                string eyeBodyLanguage = DialogFactory.NpcEyeBehavior(n);
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
            if (c.CurrentTarget is IReactable reactable)
            {
                //Display.Action($"You slap {reactable.Name}");
                reactable.OnSlapped();     
            }
            else
            {
                Display.Igm("That didn't just happen again!");  //enter combat state?
            }
        }

        public void Laugh(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IReactable reactable)
            {
                reactable.OnLaughedAt();
            }
            else
            {
                Display.Igm("There I go just laughing out loud again.");
            }
        }   

        public void Flirt(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IReactable reactable)
            {
                reactable.OnFlirtedWith();
            }
            else
            {
                Display.Igm("You can't flirt with that. Im sure its flattered though");
            }
        }

        public void Fart(List<Token> tokens, ComContext c)
        {
            if(c.CurrentTarget is IReactable reactable)
            {
                reactable.OnFartedAt();
            }
        }

        public void Help(List<Token> tokens, ComContext c)
        {
            Display.Action("Available commands: ");
            Display.Bright("pet, look, hit, slap, talk, laugh, flirt, fart, help, exit, quit, bye, leave");
            //can the list for actions build up from a dictionary and display available options?
        }

        public void Exit(List<Token> tokens, ComContext c)
        {
            Display.Igm("Ending conversation...");
            c.EndInteration = true;
        }
    }
}
