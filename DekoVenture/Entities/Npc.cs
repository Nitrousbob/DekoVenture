namespace DekoVenture
{
    public class Npc : Actor, IInspectable, ITalkable, IQuestionable, IReactable, IAttackable
    {
        public Player? CurrentPlayer { get; set; }
        public NpcIdleState IdleState { get; private set; }
        public NpcTalkingState TalkingState { get; private set; }
        public NpcBusyState BusyState { get; private set; }
        public NpcCombatState CombatState { get; private set; }

        //These are flavor text properties for Idle and Busy States
        public string IdleAction { get; set; }
        public string BusyStart { get; set; }
        public string BusyAction { get; set; }
        public string BusyEnd { get; set; }
        public string BusyRefusal { get; set; }
        public bool HasEyes { get; private set; }
        public int InteractionCount { get; set; } = 0; // This property will track how many times the NPC has greeted the player.
        public Npc(string name, int health, bool hasEyes) : base(name, health)
        {
            HasEyes = hasEyes;
            IdleState = new NpcIdleState(this);
            TalkingState = new NpcTalkingState(this);
            BusyState = new NpcBusyState(this);
            CombatState = new NpcCombatState(this);

            StateMachine.Initialize(IdleState);
        }

        public string GetDescription()
        {
            return $"You see {Name}, a character in this world. But does {Name} have character?";
            //Add a slight adjustment action here based on the weather or time of day.
            //For example, if its raining, They may pull a hood up, (get a parasol ready) or if its dark , they may light a lantern, or if its sunny,
            //they may put on sunglasses. Just a little flavor text to make the world feel more alive.
        }

        public override void OnInteract(Player player)
        {
            if (!canInteract)
            {   //maybe there are moods or other factors that could cause an NPC to not want to interact with the player, this is where that logic would go.
                UI.Narrate($"{Name} does not want to interact with you right now.");
                return;
            }

            if (StateMachine.CurrentState == BusyState)
            {
                UI.Narrate($"{Name} {BusyRefusal}");
                return;
            }

            StartTalkingTo(player);
            StateMachine.Update();
        }

        public string GetTalkResponse()
        {
            InteractionCount++;
            if (InteractionCount < 2)
            {
                return "I came here to chew bubblegum and talk, and I'm all out of bubblegum.";
            }
            else if (InteractionCount >= 2 && InteractionCount < 3)
            {
                return "Oh, you want to talk? That's nice.";
            }
            else if (InteractionCount >= 3 && InteractionCount < 4)
            {
                return "You just keep talking, don't you?";
            }
            else if (InteractionCount >= 4 && InteractionCount < 5)
            {
                return "Nowadays everybody wants to talk, like they got something to say, but when they move their lips, it just a bunch of gibberish, players act like they forgot about Dre.";
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

        public string OnAgitate()
        {
            //Display.Action($"You agitate {Name}");  srp refactor
            //may need to change BlockInteraction into Agitated
            BlockInteraction(3);  //mad for 3 turns, you can't interact with them for 3 turns.
            return $"{Name} looks at you with shock and anger.";
        }

        public string OnLaughedAt()
        {
            //Display.Action($"You laugh at {Name}");  srp refactor
            return $"{Name} laughs back at you.";
        }

        public string OnFlirtedWith()
        {
            //Display.Action($"You flirt with {Name}");  srp refactor
            return $"{Name} blushes and looks away.";
        }

        public string OnFartedAt()
        {
            //Display.Action($"You break wind in their general direction");  srp refactor

            int choice = Random.Shared.Next(3);
            switch (choice)
            {
                case 0:
                    return $"{Name} laughs at your display of bodily function.";
                case 1:
                    return $"{Name} is not amused.";
                default:
                    return $"{Name} fires back with their own brand of flatulence";
            }
        }

        public void EnterCombat(Player player)
        {
            if (StateMachine.CurrentState != CombatState && IsAlive)
            {
                //CurrentPlayer = player;
                StateMachine.ChangeState(CombatState);
            }
        }
    }


}
