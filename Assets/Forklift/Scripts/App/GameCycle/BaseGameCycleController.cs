using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Forklift.Core.SequenceExecuting;
using UnityEngine;

namespace Forklift.App.GameCycle
{
    public abstract class BaseGameCycleController : ISequenceManager, IDisposable
    {
        public event Action<ISequenceStep> OnStepStarted;
        public event Action<ISequenceStep> OnStepCompleted;
        public event Action OnSequenceStarted;
        public event Action OnSequenceStopped;
        public bool IsRunning { get; private set; }
        
        protected ISequenceStep _currentStep { get; private set; }

        private CancellationTokenSource _cts;
        private IEnumerator<ISequenceStep> _stepEnumerator;

        public async void StartExecuting()
        {
            if (IsRunning)
            {
                Debug.LogWarning("Последовательность уже выполняется");
                return;
            }

            IsRunning = true;
            _cts = new CancellationTokenSource();
            _stepEnumerator = GetSteps();

            OnSequenceStarted?.Invoke();

            try
            {
                while (_stepEnumerator.MoveNext() && !_cts.Token.IsCancellationRequested)
                {
                    _currentStep = _stepEnumerator.Current;
                    
                    await ExecuteStep(_currentStep, _cts.Token);
                    
                    if (_cts?.Token.IsCancellationRequested ?? true)
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Последовательность отменена");
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка в последовательности: {e}");
            }
            finally
            {
                StopExecuting();
            }
        }

        public void StopExecuting()
        {
            if (!IsRunning)
                return;

            IsRunning = false;

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            _stepEnumerator = null;
            _currentStep = null;

            OnSequenceStopped?.Invoke();
        }

        public void Dispose()
        {
            StopExecuting();
        }
        
        private async UniTask ExecuteStep(ISequenceStep step, CancellationToken ct)
        {
            try
            {
                OnStepStarted?.Invoke(step);

                step.Enter();
                await step.WaitForPlayerActions(ct);
                step.Exit();

                OnStepCompleted?.Invoke(step);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка в шаге: {e}");
                throw;
            }
        }
    
        protected abstract IEnumerator<ISequenceStep> GetSteps();
    }
}