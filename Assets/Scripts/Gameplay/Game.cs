using CannonSystem;
using Core;
using Data;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Game : MonoInstaller
    {
        [SerializeField] private CannonView cannonView;
        private GameplayData _data;
        
        public override void InstallBindings() =>
            Container.Bind<Game>().FromInstance(this).AsSingle().NonLazy();

        public override void Start()
        {
            base.Start();
            _data = Resources.Load<GameplayData>(Constants.DataPath);
            
            
        }

        public GameplayData GetData() => _data;
    }
}
