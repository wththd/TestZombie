namespace ZombieGame.Scripts.Systems
{
    public interface IState
    {
        void EnterState();
        void ExitState();
        GameStateType Type { get; set; }
    }
}