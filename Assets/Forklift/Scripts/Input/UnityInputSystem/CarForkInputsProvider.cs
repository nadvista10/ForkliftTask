using Zenject;

namespace Forklift.Input.UnityInputSystem
{
    public class CarForkInputsProvider : BaseCarInputProvider, ICarForkInputProvider
    {
        public CarForkInputsProvider(GameInput input) : base(input)
        {
        }

        public float GetLift()
        {
            return _gameInput.Player.Lift.ReadValue<float>();
        }
    }
}