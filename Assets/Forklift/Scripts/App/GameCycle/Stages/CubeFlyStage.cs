using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Forklift.Core.SequenceExecuting;
using UnityEngine;

namespace Forklift.App.GameCycle.Stages
{
    public class CubeFlyStage : ISequenceStep
    {
        private float _upDistance;
        private float _flyTime;
        private float _rotateDegPerSecond;

        private int _ignoreColisionLayer;
        private int _standartCubeLayer;

        private Rigidbody _cube;

        public CubeFlyStage(float upDist, float time, float rotateSpeed, Rigidbody cube, string ignoreColisionLayerName)
        {
            _upDistance = upDist;
            _flyTime = time;
            _rotateDegPerSecond = rotateSpeed;
            _cube = cube;

            _ignoreColisionLayer = LayerMask.NameToLayer(ignoreColisionLayerName);
            _standartCubeLayer = _cube.gameObject.layer;
        }

        public void Enter()
        {
            _cube.isKinematic = true;
            _cube.gameObject.layer = _ignoreColisionLayer;
        }

        public void Exit()
        {
            _cube.isKinematic = false;
            _cube.gameObject.layer = _standartCubeLayer;
        }

        public async UniTask WaitForPlayerActions(CancellationToken ct)
        {
            var newPos = _cube.transform.position; 
            newPos.y += _upDistance;
            var moveTween = _cube.transform.DOMove(newPos, _flyTime).WithCancellation(ct);

            var rotateDeg = _rotateDegPerSecond * _flyTime;
            var rotateTween = DOTween.To(
                () => _cube.transform.position.y,
                x => _cube.transform.rotation = Quaternion.Euler(x, x, x),
                rotateDeg,
                _flyTime
            ).SetEase(Ease.Linear).WithCancellation(ct);

            await UniTask.WhenAll(moveTween, rotateTween);
        }
    }
}