namespace ZombieGame.Scripts.Systems
{
    public class GameStateMachine
    {
        public GameStateType CurrentStateType => currentState.Type;
        private IState currentState;

        public void SetState(IState state)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = state;
            state.EnterState();
        }         
    }
}