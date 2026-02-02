using System;
using Forklift.Core.Car.Systems;

namespace Forklift.App.Car.Systems
{
    public class CarEngineSystem : ICarEngineSystem
    {
        public interface IEngineDataProvider
        {
            public float StandartTorque { get; }
        }

        public ICarEngineSystem.EngineStatus Status { get; set; }

        private IEngineDataProvider _data;

        public CarEngineSystem(IEngineDataProvider data)
        {
            _data = data;
        }

        public float EvaluateTorque()
        {
            if (Status == ICarEngineSystem.EngineStatus.Running)
                return _data.StandartTorque;
            return 0f;
        }
    }
}