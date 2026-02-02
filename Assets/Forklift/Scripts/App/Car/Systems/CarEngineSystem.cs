using Forklift.Core.Car.Systems;

namespace Forklift.App.Car.Systems
{
    public class CarEngineSystem : ICarEngineSystem
    {
        public interface IEngineDataProvider
        {
            public float StandartTorque { get; }
            public float TorqueToFuelConsumption { get; }
        }

        public ICarEngineSystem.EngineStatus Status { get; set; }

        private IEngineDataProvider _data;

        public CarEngineSystem(IEngineDataProvider data)
        {
            _data = data;
        }

        public float EvaluateTorque(float fuelConsumption)
        {
            if (Status == ICarEngineSystem.EngineStatus.Running)
            {
                if (_data.TorqueToFuelConsumption == 0)
                    return _data.StandartTorque;
                
                return _data.StandartTorque * fuelConsumption / _data.TorqueToFuelConsumption;
            }
            return 0f;
        }

        public float TorqueToFuelConsumption(float torque)
        {
            return torque * _data.TorqueToFuelConsumption;
        }
    }
}