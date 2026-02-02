using System;

namespace Forklift.Core.Car.Systems
{
    public interface ICarEngineSystem
    {
        public enum EngineStatus
        {
            Stopped,
            Starting,
            Running
        }

        public event Action OnStatusChange;
        public EngineStatus Status { get; set; }

        public float EvaluateTorque(float fuelConsumption);
        public float TorqueToFuelConsumption(float torque);
    }
}