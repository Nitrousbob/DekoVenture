﻿namespace RPG_Asn4
{
    //im starting to notice my Command class needs to know about the Context of the game for different commands.
    public class Command
    {
        public void Look(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is Animal a)
            {
                UI.ShowPlayerAction(a.GetDescription());
            }
            else if (c.CurrentTarget is Npc n)
            {
                UI.ShowPlayerAction(n.GetDescription());
                string eyeBodyLanguage = DialogFactory.NpcEyeBehavior(n);
                UI.Narrate($"'{eyeBodyLanguage}'");
            }
            else
            {
                UI.Narrate("There is nothing to look at.");
            }
        }
        public void Talk(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is ITalkable talkable)
            {
                UI.ShowPlayerAction($"You talk to {talkable.Name}");
                UI.Narrate(talkable.GetTalkResponse());
            }
            else
                {
                    UI.Narrate($"{c.CurrentTarget.Name} doesn't seem to be able to talk.");
                }
        }

        public void Ask(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IQuestionable questionable)
            {
                UI.ShowPlayerAction($"You ask {questionable.Name} a question.");
                UI.Narrate(questionable.GetQuestionResponse());
            }
            else
            {
                UI.Narrate($"{c.CurrentTarget} doesn't seem to be able to answer questions.");
            }
        }


        public void Pet(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IPettable pettable)
            {
                UI.ShowPlayerAction($"You pet {pettable.Name}");
                UI.Narrate(pettable.GetPetResponse());
            }
            else if (c.CurrentTarget is Npc n)
            {
                UI.ShowPlayerAction($"You try to pet {n.Name}");
                UI.Narrate($"{n.Name} doesn't seem to like that.");
            }
            else
            {
                UI.Narrate("Very unusual, that's not something you can pet.");
            }

        }

        public void Agitate(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IReactable reactable)
            {
                UI.ShowPlayerAction($"You slap {reactable.Name}");
                string reaction = reactable.OnAgitate();
                UI.ShowPlayerAction(reaction);     
            }
            else
            {
                UI.Narrate("That didn't just happen again!");  //enter combat state?
            }
        }

        public void Laugh(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IReactable reactable)
            {
                UI.ShowPlayerAction($"You laugh at {reactable.Name}");
                string reaction = reactable.OnLaughedAt();
                UI.ShowPlayerAction(reaction);
            }
            else
            {
                UI.Narrate("There I go just laughing out loud again.");
            }
        }   

        public void Flirt(List<Token> tokens, ComContext c)
        {
            if (c.CurrentTarget is IReactable reactable)
            {
                UI.ShowPlayerAction($"You flirt with {reactable.Name}");
                string reaction = reactable.OnFlirtedWith();
                UI.ShowPlayerAction(reaction);
            }
            else
            {
                UI.Narrate("You can't flirt with that. Im sure its flattered though");
            }
        }

        public void Fart(List<Token> tokens, ComContext c)
        {
            if(c.CurrentTarget is IReactable reactable)
            {
                UI.ShowPlayerAction($"You break wind in their general direction");
                string reaction = reactable.OnFartedAt();
                UI.ShowPlayerAction(reaction);
            }
        }

        public void Help(List<Token> tokens, ComContext c)
        {
            UI.ShowPlayerAction("Available commands: ");
            UI.ShowPlayerAction("pet, look, hit, slap, talk, laugh, flirt, fart, help, exit, quit, bye, leave");
            //can the list for actions build up from a dictionary and display available options?
        }

        public void Exit(List<Token> tokens, ComContext c)
        {
            UI.Narrate("Ending conversation...");
            c.EndInteration = true;
        }
    }
}
