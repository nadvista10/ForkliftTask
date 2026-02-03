using Forklift.Core.Car;
using Forklift.Core.Car.Systems;
using Forklift.Input;
using UnityEngine;
using Zenject;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarLiftController : ITickable, ICarSwitchable, IInitializable
    {
        public bool IsEnabled { get; private set; }

        private ICarLiftInputProvider _input;
        private ICarLiftSystem _lift;

        private float _progress;

        public CarLiftController(ICarLiftInputProvider input, ICarLiftSystem lift)
        {
            _input = input;
            _lift = lift;
        }

        public void Disable()
        {
            IsEnabled = false;

            _input.Disable();
        }

        public void Enable()
        {
            IsEnabled = true;

            _input.Enable();
        }

        public void Initialize()
        {
            _lift.SetLift(0);
        }

        public void Tick()
        {
            if (!IsEnabled)
                return;

            var delta = Time.deltaTime * _input.GetLift() * _lift.GetLiftSpeed();
            _progress = Mathf.Clamp01(_progress + delta);
            _lift.SetLift(_progress);
        }
    }
}