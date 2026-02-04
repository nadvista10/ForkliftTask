using System;
using Forklift.Core.Car.Systems;
using UnityEngine;

namespace Forklift.App.Car.Systems
{
    public class CarMovementSystem : ICarMovementSystem
    {
        public interface IMovementDataProvider
        {
            public Rigidbody MainBody { get; }
            public WheelCollider[] SteeringWheels { get; }
            public WheelCollider[] DriveWheels { get; }
            public WheelCollider[] BrakeWheels { get; }

            public float MoveBackToqueScale { get; }
            public float BrakeTorque { get; }
            public float AutoBrakeScale { get; }
            public float MaxSteerAngleDeg { get; }
        }
        private enum MoveStatus
        {
            Stopped,
            MoveForward,
            MoveBack
        }

        private ICarEngineSystem _engine;
        private ICarFuelSystem _fuel;
        private IMovementDataProvider _data;
        private MoveStatus _status;

        public CarMovementSystem(IMovementDataProvider data,
            ICarEngineSystem engine,
            ICarFuelSystem fuel)
        {
            _data = data;
            _engine = engine;
            _status = MoveStatus.Stopped;
            _fuel = fuel;
        }

        public void SetGas(float powerNorm, float dt)
        {
            UpdateMoveStatus();
            powerNorm = Mathf.Clamp(powerNorm, -1f, 1f);
            
            if (Mathf.Approximately(powerNorm, 0))
                ApplyBrake(_data.AutoBrakeScale);
            else if (powerNorm > 0)
                ForwardGas(powerNorm, dt);
            else
                BackwardGas(Mathf.Abs(powerNorm), dt);
        }

        public void SetSteer(float valueNorm, float dt)
        {
            var deg = Mathf.Clamp(valueNorm, -1f, 1f) * _data.MaxSteerAngleDeg;
            ApplySteer(deg);
        }

        private void ForwardGas(float powerNorm, float dt)
        {
            if (_status == MoveStatus.MoveBack)
            {
                ApplyBrake(powerNorm);
                return;
            }

            var torque = powerNorm;
            ApplyTorque(torque, dt);
        }
        private void BackwardGas(float powerNorm, float dt)
        {
            if (_status == MoveStatus.MoveForward)
            {
                ApplyBrake(powerNorm);
                return;
            }
            var torque = powerNorm * _data.MoveBackToqueScale;
            ApplyTorque(-torque, dt);
        }
        
        private void ApplyTorque(float torqueNorm, float dt)
        {
            var absTorque = Mathf.Abs(torqueNorm);

            //Ограничение мощности при <50% заполненности бака
            if(_fuel.FuelPc <= 0.5f)
                absTorque = Mathf.Min(0.5f, absTorque);

            var fuelRequired = _engine.TorqueToFuelConsumption(absTorque);
            var fuelFull = _fuel.FuelPc;

            var availableFuel = Mathf.Min(fuelFull, fuelRequired);
            var availableTorque = _engine.EvaluateTorque(availableFuel) * Mathf.Sign(torqueNorm);
            _fuel.Subtract(availableFuel * dt);

            foreach (var wheel in _data.BrakeWheels)
                wheel.brakeTorque = 0f;
            foreach (var wheel in _data.DriveWheels)
                wheel.motorTorque = availableTorque;
        }

        private void ApplyBrake(float powerNorm)
        {
            foreach (var wheel in _data.DriveWheels)
                wheel.motorTorque = 0f;
            foreach (var wheel in _data.BrakeWheels)
                wheel.brakeTorque = _data.BrakeTorque * powerNorm;
        }

        private void ApplySteer(float angle)
        {
            foreach (var wheel in _data.SteeringWheels)
                wheel.steerAngle = angle;
        }
        
        private void UpdateMoveStatus()
        {
            var velocity = _data.MainBody.linearVelocity;
            var localVelocity = _data.MainBody.transform.InverseTransformDirection(velocity);
            var forwardVelocity = localVelocity.z;

            const float threshold = 0.1f;
            if (forwardVelocity > threshold)
                _status = MoveStatus.MoveForward;
            else if (forwardVelocity < -threshold)
                _status = MoveStatus.MoveBack;
            else _status = MoveStatus.Stopped;
        }
    }
}