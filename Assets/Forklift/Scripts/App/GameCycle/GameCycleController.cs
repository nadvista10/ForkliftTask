using System;
using System.Collections.Generic;
using Forklift.App.GameCycle.Stages;
using Forklift.App.World;
using Forklift.Core.Car;
using Forklift.Core.SequenceExecuting;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Forklift.App.GameCycle
{
    public class GameCycleController : BaseGameCycleController, IInitializable
    {
        [Serializable]
        public struct GameCycleControllerData
        {
            [Header("Fade")]
            public Volume FadeVolume;
            public Color FadeStartColor;
            public Color FadeEndColor;
            public float FadeDurationSeconds;

            [Header("Cube spawn")]
            public Vector3 CubeThrowStartPosition;
            public Vector3 CubeThrowEndPosition;
            public Vector3 StandThrowStartPosition;
            public float CubeThrowTime;
            public float RotateOnThrowDegPerSecond;
            public string IgnoreColisionLayerName;

            [Header("Fly")]
            public float CubeFlyUpDistance;
            public float CubeFlyTime;
            public float RotateOnFlyDegPerSecond;

            [Header("Objects")]
            public RigidbodyWithCollider Cube;
            public TriggerObject CubeStand;
            public Transform EndColliderTransform;
        }

        private GameCycleControllerData _data;
        private List<ICarSwitchable> _switchables;

        public GameCycleController(GameCycleControllerData data, List<ICarSwitchable> switchables)
        {
            _data = data;
            _switchables = switchables;
        }

        public void Initialize()
        {
            StartExecuting();
        }

        protected override IEnumerator<ISequenceStep> GetSteps()
        {
            yield return new FadeStage(_data.FadeVolume,
                _data.FadeStartColor, _data.FadeEndColor, _data.FadeDurationSeconds);
            yield return new EnableCarControllersStage(_switchables);

            var throwStage = new CubeThrowStage(_data.CubeThrowStartPosition, _data.CubeThrowEndPosition,
                _data.StandThrowStartPosition, _data.CubeThrowTime, _data.RotateOnThrowDegPerSecond,
                _data.Cube.Rigidbody, _data.CubeStand.transform, _data.IgnoreColisionLayerName);
            var transferStage = new CubeTransferStage(_data.Cube.Collider, _data.CubeStand, _data.EndColliderTransform);
            var flyStage = new CubeFlyStage(_data.CubeFlyUpDistance, _data.CubeFlyTime,
                 _data.RotateOnFlyDegPerSecond, _data.Cube.Rigidbody, _data.IgnoreColisionLayerName);

            while(true)
            {
                yield return throwStage;
                yield return transferStage;
                yield return flyStage;
            }
        }
    }
}