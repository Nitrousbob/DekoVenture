namespace RPG_Asn4
{
    public class Npc : Actor, IInspectable, ITalkable, IQuestionable
    {
        public bool HasEyes { get; private set; }
        public int InteractionCount { get; set; } = 0; // This property will track how many times the NPC has greeted the player.
        public Npc(string name, int health, bool hasEyes) : base(name, health)
        {
            HasEyes = hasEyes;
        }

        public string GetDescription()
        {
            return $"You see {Name}, a character in this world. But does {Name} have character?";
        }
        
        public override void OnInteract(Player player)
        {
            if (!canInteract)
            {
                Display.Igm($"{Name} does not want to interact with you right now.");
                return;
            }
            
            string greeting = HumanDialogFactory.GetRandomGreeting(this);
            Display.Igm($"\n{Name} says: '{greeting}'");
            
            HumanDialogFactory.Dialogger(this, player);  //Enters the dialog
        }

        public string GetTalkResponse()
        {
            InteractionCount++;
            if (InteractionCount < 5)
            {
                return "I came here to chew bubblegum and talk, and I'm all out of bubblegum.";
            }
            else
            {
                return "I'm all out of responses now too";
            }

            
        }

        public string GetQuestionResponse()
        {
            return "I'm afraid my responses are limited, you must ask the right question.";
        }
    }
}
