using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Forklift.Compilation.GameCycle.Di
{
    public class GameCycleInstaller : MonoInstaller
    {
        [SerializeField]
        private GameCycleController.GameCycleControllerData data;

        public override void InstallBindings()
        {
            Container.BindInstance(data);
            Container.BindInterfacesAndSelfTo<GameCycleController>().FromNew().AsSingle().NonLazy();
        }
    }
}