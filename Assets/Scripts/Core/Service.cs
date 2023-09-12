using UnityEngine;
using Zenject;

namespace Core
{
    public class Service : MonoInstaller
    {
        [SerializeField] protected string _idKey;
        
        public override void InstallBindings()
        {
        }
    }
}