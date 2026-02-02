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
            Container.BindInterfacesAndSelfTo<CarData>().FromInstance(car).AsSingle();

            Container.BindInterfacesAndSelfTo<CarEngineSystem>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CarMovementSystem>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CarLiftSystem>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CarFuelSystem>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<BaseCar>().FromNew().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<CarMovementController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CarEngineController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CarLiftController>().FromNew().AsSingle().NonLazy();
        }
    }
}