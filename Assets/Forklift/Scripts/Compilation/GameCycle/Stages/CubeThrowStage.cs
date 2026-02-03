using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Forklift.App.World;
using Forklift.Core.SequenceExecuting;
using UnityEngine;

namespace Forklift.Compilation.GameCycle.Stages
{
    public class CubeThrowStage : ISequenceStep
    {
        private Vector3 _throwStartPosition;
        private Vector3 _throwEndPosition;
        private Vector3 _standStartPosition;
        private float _throwTime;
        private float _rotateDegPerSecond;

        private int _ignoreColisionLayer;
        private int _standartCubeLayer;

        private Rigidbody _cube;
        private Transform _cubeStand;

        public CubeThrowStage(Vector3 startPos, Vector3 endPos, Vector3 standStartPos,
            float time, float rotateSpeed, Rigidbody cube, Transform stand, string ignoreColisionLayerName)
        {
            _throwStartPosition = startPos;
            _throwEndPosition = endPos;
            _standStartPosition = standStartPos;
            _throwTime = time;
            _rotateDegPerSecond = rotateSpeed;
            _cube = cube;
            _cubeStand = stand;
            
            _ignoreColisionLayer = LayerMask.NameToLayer(ignoreColisionLayerName);
            _standartCubeLayer = _cube.gameObject.layer;
        }

        public void Enter()
        {
            _cube.isKinematic = true;
            _cube.gameObject.layer = _ignoreColisionLayer;
            
            _cubeStand.transform.position = _standStartPosition;
            _cubeStand.transform.rotation = Quaternion.identity;
            _cube.transform.rotation = Quaternion.identity;
            _cube.transform.position = _throwStartPosition;
        }

        public void Exit()
        {
            _cube.isKinematic = false;
            _cube.gameObject.layer = _standartCubeLayer;
        }

        public async UniTask WaitForPlayerActions(CancellationToken ct)
        {
            var moveTween = _cube.transform.DOMove(_throwEndPosition, _throwTime).WithCancellation(ct);

            var rotateDeg = _rotateDegPerSecond * _throwTime;
            var rotateTween = DOTween.To(
                () => _cube.transform.position.y,
                x => _cube.transform.rotation = Quaternion.Euler(0, x, 0),
                rotateDeg,
                _throwTime
            ).SetEase(Ease.OutCubic).WithCancellation(ct);

            await UniTask.WhenAll(moveTween, rotateTween);
        }
    }
}