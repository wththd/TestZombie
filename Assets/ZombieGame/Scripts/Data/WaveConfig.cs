using UnityEngine;

namespace ZombieGame.Scripts.Data
{
    [CreateAssetMenu]
    public class WaveConfig : ScriptableObject
    {
        public int MaxEnemies;
        public int TotalEnemies;
    }
}