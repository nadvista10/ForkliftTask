using System;

namespace Forklift.Input.UnityInputSystem
{
    public class BaseCarInputProvider : IInputProvider, IDisposable
    {
        public bool IsEnabled { get; private set; }

        protected readonly GameInput _gameInput;

        public BaseCarInputProvider(GameInput input)
        {
            _gameInput = input;
        }

        public void Disable()
        {
            _gameInput.Disable();
            IsEnabled = false;

            DisableInternal();
        }

        public void Enable()
        {
            _gameInput.Enable();
            IsEnabled = true;

            EnableInternal();
        }

        public void Dispose()
        {
            _gameInput.Dispose();

            DisposeInternal();
        }

        protected virtual void EnableInternal() { }
        protected virtual void DisableInternal() { }
        protected virtual void DisposeInternal() { }
    }
}