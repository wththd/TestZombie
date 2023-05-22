using System.Collections.Generic;

namespace ZombieGame.Scripts.Systems
{
    public class PauseController : IPauseController
    {
        private readonly List<IPausable> pausables = new();
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            foreach (var pausable in pausables) pausable.OnPause();

            IsPaused = true;
        }

        public void Resume()
        {
            foreach (var pausable in pausables) pausable.OnResume();

            IsPaused = false;
        }

        public bool RegisterPausable(IPausable pausable)
        {
            pausables.Add(pausable);
            return IsPaused;
        }

        public void UnregisterPausable(IPausable pausable)
        {
            pausables.Remove(pausable);
        }
    }
}