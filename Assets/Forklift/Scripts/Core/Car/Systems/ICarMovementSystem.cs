namespace Forklift.Core.Car.Systems
{
    public interface ICarMovementSystem
    {
        public void SetGas(float powerNorm);
        public void SetSteer(float valueNorm);
    }
}