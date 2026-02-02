using System;
using UnityEngine.InputSystem;

namespace Forklift.Input.UnityInputSystem
{
    public class CarEngineInputsProvider : BaseCarInputProvider, ICarEngineInputsProvider
    {
        public event Action OnSwitchButtonPress;
        public event Action<float> OnSwitchButtonRelease;

        public CarEngineInputsProvider(GameInput input) : base(input)
        {
        }

        protected override void EnableInternal()
        {
            _gameInput.Player.SwitchEngine.started += OnSwitchStarted;
            _gameInput.Player.SwitchEngine.canceled += OnSwitchCanceled;
        }

        protected override void DisableInternal()
        {
            _gameInput.Player.SwitchEngine.started -= OnSwitchStarted;
            _gameInput.Player.SwitchEngine.canceled -= OnSwitchCanceled;
        }

        protected override void DisposeInternal()
        {
            DisableInternal();
        }

        private void OnSwitchStarted(InputAction.CallbackContext ctx) 
            => OnSwitchButtonPress?.Invoke();
        private void OnSwitchCanceled(InputAction.CallbackContext ctx) 
            => OnSwitchButtonRelease?.Invoke((float)ctx.duration);
    }
}