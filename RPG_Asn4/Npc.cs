namespace RPG_Asn4
{
    public class Npc : Actor, IInspectable, ITalkable, IQuestionable, IReactable
    {
        public Player? CurrentPlayer { get; set; }
        public NpcIdleState IdleState { get; private set; }
        public NpcTalkingState TalkingState { get; private set; }
        public NpcBusyState BusyState { get; private set; }

        //These are flavor text properties for Idle and Busy States
        public string IdleAction {get; set;}
        public string BusyStart {get; set;}
        public string BusyAction {get; set;}
        public string BusyEnd {get; set;}
        public string BusyRefusal {get; set;}

        public bool HasEyes { get; private set; }
        public int InteractionCount { get; set; } = 0; // This property will track how many times the NPC has greeted the player.
        public Npc(string name, int health, bool hasEyes) : base(name, health)
        {
            HasEyes = hasEyes;
            IdleState = new NpcIdleState(this);
            TalkingState = new NpcTalkingState(this);
            BusyState = new NpcBusyState(this);

            StateMachine.Initialize(IdleState);
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
            
            if (StateMachine.CurrentState == BusyState)
            {
                Display.Igm($"{Name} {BusyRefusal}");
                return;
            }

            StartTalkingTo(player);
            StateMachine.Update();
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

        public void StartTalkingTo(Player player)
        {
            CurrentPlayer = player;
            StateMachine.ChangeState(TalkingState);
        }

        public void OnSlapped()
        {
            Display.Action($"You slap {Name}");
            Display.DarkAction($"{Name} looks at you with shock and anger.");
            BlockInteraction(3);  //mad for 3 turns, you can't interact with them for 3 turns.
        }

        public void OnLaughedAt()
        {
            Display.Action($"You laugh at {Name}");
            Display.DarkAction($"{Name} laughs back at you.");
        }

        public void OnFlirtedWith()
        {
            Display.Action($"You flirt with {Name}");
            Display.DarkAction($"{Name} blushes and looks away.");

        }

        public void OnFartedAt()
        {
            Display.Action($"You break wind in their general direction");

                int choice = Random.Shared.Next(3);
                switch (choice)
                {
                    case 0:
                        Display.DarkAction($"{Name} laughs at your display of bodily function.");
                        break;
                    case 1:
                        Display.DarkAction($"{Name} is not amused.");
                        break;
                    case 2:
                        Display.DarkAction($"{Name} fires back with their own brand of flatulence");
                        break;
                }
        }
    }
}
