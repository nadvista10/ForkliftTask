using Forklift.App.Car;
using Forklift.App.Car.Systems;
using Forklift.Compilation.Car.Controllers;
using UnityEngine;
using Zenject;

namespace Forklift.Compilation.Car.Di
{
    public class CarInstaller : MonoInstaller
    {
        [SerializeField]
        private CarData car;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CarData>().FromInstance(car).AsSingle();

            Container.BindInterfacesTo<CarEngineSystem>().FromNew().AsSingle();
            Container.BindInterfacesTo<CarMovementSystem>().FromNew().AsSingle();
            Container.BindInterfacesTo<CarLiftSystem>().FromNew().AsSingle();
            Container.BindInterfacesTo<CarFuelSystem>().FromNew().AsSingle();

            Container.BindInterfacesTo<BaseCar>().FromNew().AsSingle().NonLazy();

            Container.BindInterfacesTo<CarMovementController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<CarEngineStartController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<CarLiftController>().FromNew().AsSingle().NonLazy();
        }
    }
}