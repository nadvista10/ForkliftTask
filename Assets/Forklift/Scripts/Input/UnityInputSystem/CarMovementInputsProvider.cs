using UnityEngine;

namespace Forklift.Input.UnityInputSystem
{
    public class CarMovementInputsProvider : BaseCarInputProvider, ICarMovementInputProvider
    {
        public CarMovementInputsProvider(GameInput input) : base(input)
        {
        }

        public float GetSteer()
        {
            var input = _gameInput.Player.Steer.ReadValue<float>();
            return Mathf.Clamp(input, -1, 1);
        }

        public float GetMove()
        {
            var input = _gameInput.Player.Move.ReadValue<float>();
            return Mathf.Clamp(input, -1, 1);
        }
    }
}