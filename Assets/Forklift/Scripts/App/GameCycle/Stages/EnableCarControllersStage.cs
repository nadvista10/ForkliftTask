using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Forklift.Core.Car;
using Forklift.Core.SequenceExecuting;

namespace Forklift.App.GameCycle.Stages
{
    public class EnableCarControllersStage : ISequenceStep
    {
        private List<ICarSwitchable> _switchables;

        public EnableCarControllersStage(List<ICarSwitchable> switchables)
        {
            _switchables = switchables;
        }

        public void Enter()
        {
            foreach (var switchable in _switchables)
                switchable.Enable();
        }

        public void Exit()
        {
        }

        public async UniTask WaitForPlayerActions(CancellationToken ct)
        {
            await UniTask.CompletedTask;
        }
    }
}