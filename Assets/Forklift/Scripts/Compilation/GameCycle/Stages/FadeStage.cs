using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Forklift.Core.SequenceExecuting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Forklift.Compilation.GameCycle.Stages
{
    public class FadeStage : ISequenceStep
    {
        private Volume _volume;
        private ColorAdjustments _ca;

        private Color _start;
        private Color _end;
        private float _duration;

        private Tween _tween;

        public FadeStage(Volume volume, Color start, Color end, float duration)
        {
            _volume = volume;
            _start = start;
            _end = end;
            _duration = duration;
        }

        public void Enter()
        {
            if (!_volume.profile.TryGet<ColorAdjustments>(out _ca))
                _ca = _volume.profile.Add<ColorAdjustments>(true);

            _ca.active = true;
            _ca.postExposure.overrideState = false;
            _ca.contrast.overrideState = false;
            _ca.colorFilter.overrideState = true;
            _ca.hueShift.overrideState = false;
            _ca.saturation.overrideState = false;

            _ca.colorFilter.value = _start;
        }

        public void Exit()
        {
            if (_ca == null)
                return;
            _ca.active = false;
            _ca.colorFilter.value = _end;
        }

        public async UniTask WaitForPlayerActions(CancellationToken ct)
        {
            _tween = DOTween.To(
                    () => _ca.colorFilter.value,
                    x => _ca.colorFilter.value = x,
                    _end,
                    _duration
                )
                .SetEase(Ease.InSine);

            await _tween.WithCancellation(ct);
        }
    }
}