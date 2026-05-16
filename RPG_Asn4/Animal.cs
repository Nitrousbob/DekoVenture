namespace RPG_Asn4
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

        public string GetPetResponse()
        {
            return $"{Name} seems to enjoy the petting.";
        }

        public override void OnInteract(Player player)
        {
            if (!canInteract)
            {
            Display.Igm($"{Name} signals that this is not a game.");
            return;
            }
                             
            CurrentPlayer = player;
            StateMachine.ChangeState(InteractingState);
            StateMachine.Update();
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
            Display.DarkAction($"{Name} tilts its head, confused by the noise.");
        }

        public void OnFlirtedWith()
        {
            Display.Action($"You try to flirt with {Name}");
            Display.DarkAction($"{Name} ignores you completely.");
        }

        public void OnFartedAt()
        {
            //nothing happens
        }
    }
}
