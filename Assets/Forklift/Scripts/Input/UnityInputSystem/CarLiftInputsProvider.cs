namespace Forklift.Input.UnityInputSystem
{
    public class CarLiftInputsProvider : BaseCarInputProvider, ICarLiftInputProvider
    {
        public CarLiftInputsProvider(GameInput input) : base(input)
        {
        }

        public float GetLift()
        {
            return _gameInput.Player.Lift.ReadValue<float>();
        }
    }
}