namespace RPG_Asn4
{
    public class Door : Actor, IDestructible, IInspectable
    {
        public string Description { get; set; }
        public DoorClosedState ClosedState { get; private set; }
        public DoorOpenState OpenState { get; private set; }
        public DoorLockedState LockedState { get; private set; }

        public Door(string name, int health, string description) : base(name, health)
        {
            Description = description;
            ClosedState = new DoorClosedState(this);
            OpenState = new DoorOpenState(this);
            LockedState = new DoorLockedState(this);

            StateMachine.Initialize(ClosedState);
        }

        public string GetDescription() => Description;

        public override void OnInteract(Player player)
        {
            if (StateMachine.CurrentState == ClosedState)
            {
                UI.Narrate($"You push the {Name} open. It creaks loudly.");
                StateMachine.ChangeState(OpenState);
            }
            else if (StateMachine.CurrentState == OpenState)
            {
                UI.Narrate($"You pull the {Name} shut.");
                StateMachine.ChangeState(ClosedState);
            }
            else if (StateMachine.CurrentState == LockedState)
            {
                UI.Narrate($"You rattle the handle, but the {Name} is firmly locked.");
            }
        }

        public override void TakeDamage(int damage)
        {
            Vitals.TakeDamage(damage);
            UI.Narrate($"You attack the {Name}! (Health: {Health})");
            
            if (!IsAlive)
            {
                UI.Narrate($"The {Name} is destroyed!");
            }
        }
    }

    public class DoorClosedState : IState
    {
        private readonly Door _door;
        public string Name => "Closed";
        public DoorClosedState(Door door)
        {
            _door = door;
        }

        public void Enter(){}
        public void Update(){}
        public void Exit(){}
    }

    public class DoorOpenState : IState
    {
        private readonly Door _door;
        public string Name => "Open";
        public DoorOpenState(Door door)
        {
            _door = door;
        }
        public void Enter(){}
        public void Update(){}
        public void Exit(){}
    }

    public class DoorLockedState : IState
    {
        private readonly Door _door;
        public string Name => "Locked";
        public DoorLockedState(Door door)
        {
            _door = door;
        }
        public void Enter(){}
        public void Update(){}
        public void Exit(){}
    }
    public class BrittleDoor : Door
    {
        public BrittleDoor(string name, int health, string description = "A sturdy door, but not made of something I cannot break.") : base(name, health, description)
        {
        }

        public override void TakeDamage(int damage)
        {
            Vitals.TakeDamage(damage);
            UI.Narrate($"You chop at the {Name}! (Health: {Health})");
            
            if (!IsAlive)
            {
                UI.Narrate("The door shatters to pieces!");
            }
        }
    }
}