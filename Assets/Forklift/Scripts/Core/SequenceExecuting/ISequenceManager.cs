using System;

namespace Forklift.Core.SequenceExecuting
{
    public interface ISequenceManager
    {
        public event Action<ISequenceStep> OnStepStarted;
        public event Action<ISequenceStep> OnStepCompleted;
        public event Action OnSequenceStarted;
        public event Action OnSequenceStopped;
    
        public bool IsRunning { get; }
        public void StartExecuting();
        public void StopExecuting();
    }
}