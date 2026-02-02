using System;
using Forklift.Core.Car.Systems;
using Forklift.Input;
using Zenject;

namespace Forklift.Compilation.Car.Controllers
{
    public class CarEngineController : IInitializable, IDisposable
    {
        private ICarEngineInputsProvider _input;
        private ICarEngineSystem _engine;

        public CarEngineController(ICarEngineInputsProvider input, ICarEngineSystem engine)
        {
            _input = input;
            _engine = engine;
        }

        public void Initialize()
        {
            _input.Enable();
            _input.OnSwitchButtonPress += OnSwitchPress;
        }

        public void Dispose()
        {
            _input.OnSwitchButtonPress -= OnSwitchPress;
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