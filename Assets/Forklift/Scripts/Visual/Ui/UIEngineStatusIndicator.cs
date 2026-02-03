using System.Collections.Generic;
using Forklift.Core.Car.Systems;
using TMPro;
using UnityEngine;
using Zenject;

namespace Forklift.Visual.Ui
{
    public class UIEngineStatusIndicator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI label;

        [SerializeField]
        private string messageFormat;

        [Inject]
        private ICarEngineSystem _engine;

        private Dictionary<ICarEngineSystem.EngineStatus, string> _statusToMessage = new();

        private void OnEnable()
        {
            _engine.OnStatusChange += OnStatusChange;
            OnStatusChange();
        }

        private void OnDisable()
        {
            _engine.OnStatusChange -= OnStatusChange;
        }

        private void OnStatusChange()
        {
            var status = _engine.Status;
            if (!_statusToMessage.ContainsKey(status))
            {
                var stringStatus = string.Format(messageFormat, _engine.Status.ToString());
                _statusToMessage.Add(status, stringStatus);

                label.text = stringStatus;
            }
            else
            {
                label.text = _statusToMessage[status];
            }
        }
    }
}