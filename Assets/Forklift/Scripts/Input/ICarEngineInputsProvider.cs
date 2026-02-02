using System;

namespace Forklift.Input
{
    public interface ICarEngineInputsProvider : IInputProvider
    {
        public event Action OnSwitchButtonPress;
        public event Action<float> OnSwitchButtonRelease;
    }
}