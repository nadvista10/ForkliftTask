using System.Threading;
using Cysharp.Threading.Tasks;

namespace Forklift.Core.SequenceExecuting
{
    public interface ISequenceStep
    {
        public void Enter();
        public UniTask WaitForPlayerActions(CancellationToken ct);
        public void Exit();
    }
}