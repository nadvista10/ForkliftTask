using Forklift.Core.Car.Systems;
using Forklift.Input;
using UnityEngine;
using Zenject;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarMovementController : ITickable, IInitializable
    {
        private ICarMovementInputProvider _input;
        private ICarMovementSystem _movement;

        public CarMovementController(ICarMovementInputProvider input, ICarMovementSystem movement)
        {
            _input = input;
            _movement = movement;
        }

        public void Initialize()
        {
            _input.Enable();
        }

        public void Tick()
        {
            var gasInput = _input.GetMove();
            _movement.SetGas(gasInput, Time.deltaTime);

            var steerInput = _input.GetSteer();
            _movement.SetSteer(steerInput, Time.deltaTime);
        }
    }
}