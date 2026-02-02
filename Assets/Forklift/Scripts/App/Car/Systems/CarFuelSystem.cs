using System;
using Forklift.Core.Car.Systems;
using UnityEngine;
using Zenject;

namespace Forklift.App.Car.Systems
{
    public class CarFuelSystem : ICarFuelSystem, IInitializable
    {
        public event Action OnUpdate;
        public float FuelPc { get;  private set;}

        public void Reset()
        {
            FuelPc = 1f;
            OnUpdate?.Invoke();
        }

        public void Subtract(float amountPc)
        {
            if (amountPc == 0)
                return;

            amountPc = Mathf.Clamp01(amountPc);
            FuelPc = Mathf.Clamp01(FuelPc - amountPc);
            OnUpdate?.Invoke();
        }

        public void Initialize()
        {
            Reset();
        }
    }
}