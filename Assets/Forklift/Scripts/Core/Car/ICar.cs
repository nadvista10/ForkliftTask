using Forklift.Core.Car.Systems;

namespace Forklift.Core.Car
{
    public interface ICar
    {
        public IEngineSystem Engine { get; }
        public IMovementSystem Movement { get; }
    }
}