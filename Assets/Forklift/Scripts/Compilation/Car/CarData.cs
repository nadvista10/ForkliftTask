using Forklift.App.Car.Systems;
using UnityEngine;

namespace Forklift.Compilation.Car
{
    public class CarData : MonoBehaviour, CarEngineSystem.IEngineDataProvider, CarMovementSystem.IMovementDataProvider
    {
        [field: Header("Parts")]
        [field: SerializeField]
        public Rigidbody MainBody { get; private set; }

        [field: SerializeField]
        public WheelCollider[] SteeringWheels { get; private set; }

        [field: SerializeField]
        public WheelCollider[] DriveWheels { get; private set; }

        [field: SerializeField]
        public WheelCollider[] BrakeWheels { get; private set; }

        [field: Header("Brake & Transmission")]
        [field: SerializeField]
        [field: Range(0, 1f)]
        public float MoveBackToqueScale { get; private set; }

        [field: SerializeField]
        [field: Min(0f)]
        public float BrakeTorque { get; private set; }
        
        [field: SerializeField]
        [field: Range(0, 1f)]
        public float AutoBrakeScale { get; private set; }

        [field: Header("Steer")]
        [field: SerializeField]
        [field: Range(0, 60f)]
        public float MaxSteerAngleDeg { get; private set; }

        [field: Header("Engine data")]
        [field: SerializeField]
        [field: Min(0f)]
        public float StandartTorque { get; private set; }
    }
}