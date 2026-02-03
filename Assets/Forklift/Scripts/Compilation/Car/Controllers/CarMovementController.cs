using Forklift.Core.Car;
using Forklift.Core.Car.Systems;
using Forklift.Input;
using UnityEngine;
using Zenject;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarMovementController : ITickable, ICarSwitchable
    {
        public bool IsEnabled { get; private set; }

        private ICarMovementInputProvider _input;
        private ICarMovementSystem _movement;

        public CarMovementController(ICarMovementInputProvider input, ICarMovementSystem movement)
        {
            _input = input;
            _movement = movement;
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

        public void Tick()
        {
            if (!IsEnabled)
                return;
                
            var gasInput = _input.GetMove();
            _movement.SetGas(gasInput, Time.deltaTime);

            var steerInput = _input.GetSteer();
            _movement.SetSteer(steerInput, Time.deltaTime);
        }
    }
}