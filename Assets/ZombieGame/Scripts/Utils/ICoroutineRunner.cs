using System.Collections;
using UnityEngine;

namespace ZombieGame.Scripts.Utils
{
    public interface ICoroutineRunner
    {
        Coroutine RunCoroutine(IEnumerator coroutine);
    }
}