using RPG_Asn4;

namespace RPG_Asn4
{
    public class AnimalForagingState : IState
    {
    private readonly Animal animal;
    public string Name => "Foraging";

    public AnimalForagingState(Animal animal)
    {
        this.animal = animal;
    }

    public void Enter() {}
    public void Update()
        {
            int chance = Random.Shared.Next(100);
            if(chance < 20) //20 percent chance to go to sleep
            {
                animal.StateMachine.ChangeState(animal.SleepingState);
            }
            else if (chance < 80)
            {
                UI.ShowNpcAction($"{animal.Name} is foraging.");
            }
        }
        public void Exit() {}
    }

    public class AnimalSleepingState : IState
    {
        private readonly Animal animal;
        public string Name => "Sleeping";

        public AnimalSleepingState(Animal animal)
        {
            this.animal = animal;
        }

        public void Enter()
        {
            UI.ShowNpcAction($"{animal.Name} lies down and goes to sleep.");
        }
        public void Update()
        {
            if (Random.Shared.Next(100) < 30) // 30% chance to wake up
            {
                animal.StateMachine.ChangeState(animal.ForagingState);
            }
            else
            {
                UI.ShowNpcAction($"{animal.Name} is sleeping.");
            }
        }
        public void Exit()
        {
            UI.ShowNpcAction($"{animal.Name} wakes up and stretches.");
        }
    }

    public class AnimalInteractingState : IState
    {
        private readonly Animal animal;
        public string Name => "Interacting";

        public AnimalInteractingState(Animal animal)
        {
            this.animal = animal;
        }

        public void Enter()
        {
            UI.Narrate($"{animal.Name} makes {AnimalDialogFactory.GetRandomAnimalNoise()}");
        }
        public void Update()
        {
            if(animal.CurrentPlayer == null)
            {
                animal.StateMachine.ChangeState(animal.ForagingState);
                return;
            }
            bool isInteracting = true;
            while (isInteracting)
            {
                isInteracting = DialogFactory.HandleDialogTurn(animal, animal.CurrentPlayer);
            }
            animal.CurrentPlayer = null;
            animal.StateMachine.ChangeState(animal.ForagingState);
        }
        public void Exit() {}
    }
}

     