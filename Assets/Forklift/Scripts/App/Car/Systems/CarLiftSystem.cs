using Forklift.Core.Car.Systems;
using UnityEngine;

namespace Forklift.App.Car.Systems
{
    public class CarLiftSystem : ICarLiftSystem
    {
        public interface ILiftDataProvider
        {
            public float LiftSpeed { get; }
            public Vector3 TopPositionLocal { get; }
            public Vector3 BotPositionLocal { get; }

            public Transform Lift { get; }
        }

        private ILiftDataProvider _data;

        public CarLiftSystem(ILiftDataProvider data)
        {
            _data = data;
        }

        public void SetLift(float valueNorm)
        {
            valueNorm = Mathf.Clamp01(valueNorm);

            var newPosition = Vector3.Lerp(_data.BotPositionLocal, _data.TopPositionLocal, valueNorm);
            _data.Lift.transform.localPosition = newPosition;
        }

        public float GetLiftSpeed()
        {
            return _data.LiftSpeed;
        }
    }
}