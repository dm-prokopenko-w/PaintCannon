using System.Collections.Generic;
using Core;
using UnityEngine;

namespace WallsSystem
{
    public class WallsService : Service
    {
        [SerializeField] private List<Collider> _walls;

        public override void InstallBindings()
        {
            Container.Bind<WallsService>().FromInstance(this).AsSingle().NonLazy();
        }

        public bool IsHitWall(Collider col)
        {
            if (_walls == null || _walls.Count <= 0) return false;

            return _walls.Find(x => x == col) != null;
        }
    }
}