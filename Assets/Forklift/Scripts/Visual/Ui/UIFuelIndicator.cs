using System;
using Forklift.Core.Car.Systems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Forklift.Visual.Ui
{
    public class UIFuelIndicator : MonoBehaviour
    {
        [SerializeField]
        private Image imageIndicator;

        [Inject]
        private ICarFuelSystem _fuel;

        void Awake()
        {
            imageIndicator.type = Image.Type.Filled;
        }

        private void OnEnable()
        {
            _fuel.OnUpdate += OnFuelUpdate;
            OnFuelUpdate();
        }

        private void OnDisable()
        {
            _fuel.OnUpdate -= OnFuelUpdate;
        }

        private void OnFuelUpdate()
        {
            imageIndicator.fillAmount = _fuel.FuelPc;
        }
    }
}