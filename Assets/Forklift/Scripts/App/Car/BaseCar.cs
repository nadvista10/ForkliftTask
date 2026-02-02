using Forklift.Core.Car;
using Forklift.Core.Car.Systems;

namespace Forklift.App.Car
{
    public class BaseCar : ICar
    {
        public ICarEngineSystem Engine { get; private set; }
        public ICarMovementSystem Movement { get; private set; }
        public ICarForkSystem Fork { get; private set; }
        
        public BaseCar(ICarEngineSystem engine, ICarMovementSystem movement, ICarForkSystem fork)
        {
            Engine = engine;
            Movement = movement;
            Fork = fork;
        }
    }
}