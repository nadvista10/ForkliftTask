using System.Threading;
using Cysharp.Threading.Tasks;
using Forklift.App.World;
using Forklift.Core.SequenceExecuting;
using UnityEngine;

namespace Forklift.App.GameCycle.Stages
{
    public class CubeTransferStage : ISequenceStep
    {
        private Collider _cube;
        private TriggerObject _stand;
        private Transform _endColliderTransform;

        private UniTaskCompletionSource _tcs;

        public CubeTransferStage(Collider cube, TriggerObject stand, Transform endCollider)
        {
            _stand = stand;
            _cube = cube;
            _endColliderTransform = endCollider;
        }

        public void Enter()
        {
            _stand.TriggerEnter += OnCubeTriggerEnter;
            _tcs = new UniTaskCompletionSource();
        }

        public void Exit()
        {
            _stand.TriggerEnter -= OnCubeTriggerEnter;
        }

        public async UniTask WaitForPlayerActions(CancellationToken ct)
        {
            await UniTask.WhenAny(_tcs.Task, UniTask.WaitUntilCanceled(ct));
        }

        private void OnCubeTriggerEnter(Collider collider)
        {
            if(collider.transform == _endColliderTransform && _stand.IsColliding(_cube))
            {
                _tcs?.TrySetResult();
            }
        }
    }
}