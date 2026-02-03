using System;
using System.Collections.Generic;
using Forklift.App.GameCycle;
using Forklift.Compilation.GameCycle.Stages;
using Forklift.Core.Car;
using Forklift.Core.SequenceExecuting;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Forklift.Compilation.GameCycle
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
        }
    }
}