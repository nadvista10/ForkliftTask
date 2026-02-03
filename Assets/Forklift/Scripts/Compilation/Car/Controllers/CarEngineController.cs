using System;
using Forklift.Core.Car;
using Forklift.Core.Car.Systems;
using Forklift.Input;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarEngineController : ICarSwitchable
    {
        public bool IsEnabled { get; private set; }

        private ICarEngineInputsProvider _input;
        private ICarEngineSystem _engine;

        public CarEngineController(ICarEngineInputsProvider input, ICarEngineSystem engine)
        {
            _input = input;
            _engine = engine;
        }

        public void Enable()
        {
            IsEnabled = true;

            _input.Enable();
            _input.OnSwitchButtonPress += OnSwitchPress;
        }

        public void Disable()
        {
            IsEnabled = false;

            _input.Disable();
            _input.OnSwitchButtonPress += OnSwitchPress;
        }

        private void OnSwitchPress()
        {
            NextEngineStatus();
        }

        private void NextEngineStatus()
        {
            _engine.Status = _engine.Status switch
            {
                ICarEngineSystem.EngineStatus.Stopped => ICarEngineSystem.EngineStatus.Running,
                ICarEngineSystem.EngineStatus.Starting => ICarEngineSystem.EngineStatus.Running,
                ICarEngineSystem.EngineStatus.Running => ICarEngineSystem.EngineStatus.Stopped,
                _ => throw new NotImplementedException(),
            };
        }
    }
}