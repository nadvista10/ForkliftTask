using UnityEngine;
using Zenject;

namespace Forklift.App.GameCycle.Di
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