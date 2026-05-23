namespace DekoVenture
{
    public class Animal : Actor, IInspectable, IPettable, IReactable
    {
        public string Species { get; private set; }
        public bool HasEyes { get; private set; }
        public Player? CurrentPlayer { get; set; }

        public AnimalForagingState ForagingState {get; private set;}
        public AnimalSleepingState SleepingState {get; private set;}
        public AnimalInteractingState InteractingState {get; private set;}


        public Animal(string name, int health, string species, bool hasEyes) : base(name, health)
        {
            Species = species;
            HasEyes = hasEyes;

            ForagingState = new AnimalForagingState(this);
            SleepingState = new AnimalSleepingState(this);
            InteractingState = new AnimalInteractingState(this);

            StateMachine.Initialize(ForagingState);
        }

        public string GetDescription()
        {
            return $"You see {Name}, a {Species} in this world?";
        }

        public virtual string GetPetResponse()
        {
            return $"{Name} seems to enjoy the petting.";
        }

        public override void OnInteract(Player player)
        {
            if (!canInteract)
            {
            UI.Narrate($"{Name} signals that this is not a game.");
            return;
            }
                             
            CurrentPlayer = player;
            StateMachine.ChangeState(InteractingState);
            StateMachine.Update();
        }

        public string OnAgitate()
        {
                BlockInteraction(3);  //mad for 3 turns, you can't interact with them for 3 turns.
                return $"{Name} looks at you with shock and anger.";
        }

        public string OnLaughedAt()
        {
            return $"{Name} tilts its head, confused by the noise.";
        }

        public string OnFlirtedWith()
        {
            return $"{Name} ignores you completely.";
        }

        public string OnFartedAt()
        {
            return $"{Name} just stares at you, unfazed.";
        }
    }

    public class SecretAnimal : Animal
    {
        public Item HiddenItem {get; set;}
        private bool _secretRevealed = false;

        public SecretAnimal(string name, int health, string species, bool hasEyes, Item hiddenItem) : base(name, health, species, hasEyes)
        {
            HiddenItem = hiddenItem;
        }

        public override string GetPetResponse()
        {
            if (!_secretRevealed)
            {
                _secretRevealed = true;
                Game.CurrentGame?.CurrentZone?.CurrentLocation.Interactables.Add(HiddenItem);
                return $"{base.GetPetResponse()}\n{Name} happily paws at the dirt, revealing a {HiddenItem.Name}.";
            }
                return $"base.GetPetResponse()";
        }
    }
}
