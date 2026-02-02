namespace Forklift.Core.Car.Systems
{
    public interface ICarMovementSystem
    {
        public void SetGas(float powerNorm, float dt);
        public void SetSteer(float valueNorm, float dt);
    }
}