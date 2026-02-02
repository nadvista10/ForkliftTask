using System;

namespace Forklift.Core.Car.Systems
{
    public interface ICarFuelSystem
    {
        public event Action OnUpdate;
        public float FuelPc { get; }
        public void Reset();
        public void Subtract(float amountPc);
    }
}