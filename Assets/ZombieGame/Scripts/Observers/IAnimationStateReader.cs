namespace ZombieGame.Scripts.Observers
{
    internal interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}