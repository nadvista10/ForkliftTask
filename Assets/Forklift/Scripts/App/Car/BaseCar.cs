using Forklift.Core.Car;
using Forklift.Core.Car.Systems;

namespace Forklift.App.Car
{
    public class BaseCar : ICar
    {
        public ICarEngineSystem Engine { get; private set; }
        public ICarMovementSystem Movement { get; private set; }
        public ICarLiftSystem Lift { get; private set; }
        public ICarFuelSystem Fuel { get; private set; }
        
        public BaseCar(ICarEngineSystem engine,
            ICarMovementSystem movement,
            ICarLiftSystem fork,
            ICarFuelSystem fuel)
        {
            Engine = engine;
            Movement = movement;
            Lift = fork;
            Fuel = fuel;
        }
    }
}