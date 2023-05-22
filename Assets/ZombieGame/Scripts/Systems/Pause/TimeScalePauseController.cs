using UnityEngine;

namespace ZombieGame.Scripts.Systems.Pause
{
    public class TimeScalePauseController : IPauseController
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}