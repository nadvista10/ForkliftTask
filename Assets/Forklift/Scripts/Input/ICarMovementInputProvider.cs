namespace Forklift.Input
{
    public interface ICarMovementInputProvider : IInputProvider
    {
        public float GetMove();
        public float GetSteer();
    }
}