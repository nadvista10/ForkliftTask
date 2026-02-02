using Forklift.Core.Car.Systems;

namespace Forklift.Core.Car
{
    public interface ICar
    {
        public ICarEngineSystem Engine { get; }
        public ICarMovementSystem Movement { get; }
        public ICarForkSystem Fork { get; }
    }
}