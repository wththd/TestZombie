using System;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;

namespace ZombieGame.Scripts.Installers
{
    public abstract class BaseRunner : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected virtual void Awake()
        {
            throw new NotImplementedException();
        }
    }
}