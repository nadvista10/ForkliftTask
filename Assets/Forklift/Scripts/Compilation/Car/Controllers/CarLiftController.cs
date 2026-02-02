using Forklift.App.Car.Systems;
using Forklift.Core.Car.Systems;
using Forklift.Input;
using UnityEngine;
using Zenject;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarLiftController : ITickable, IInitializable
    {
        private ICarLiftInputProvider _input;
        private ICarLiftSystem _lift;
        private CarLiftSystem.ILiftDataProvider _liftData;

        private float _progress;

        public CarLiftController(ICarLiftInputProvider input, ICarLiftSystem lift, CarLiftSystem.ILiftDataProvider data)
        {
            _input = input;
            _lift = lift;
            _liftData = data;
        }

        public void Initialize()
        {
            _input.Enable();
            _lift.SetLift(0);
        }

        public void Tick()
        {
            var delta = Time.deltaTime * _input.GetLift() * _liftData.LiftSpeed;
            _progress = Mathf.Clamp01(_progress + delta);
            _lift.SetLift(_progress);
        }
    }
}