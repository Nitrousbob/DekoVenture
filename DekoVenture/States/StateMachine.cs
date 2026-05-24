namespace DekoVenture
{
    public interface IState
    {
        string Name { get; }
        void Enter();
        void Update();
        void Exit();
    }

    public class StateMachine
    {
        public IState? CurrentState { get; private set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }

}
