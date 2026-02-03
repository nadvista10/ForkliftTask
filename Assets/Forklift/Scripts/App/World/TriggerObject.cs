using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forklift.App.World
{
    public class TriggerObject : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        public IEnumerable<Collider> EnteredObjects => _entered;

        private List<Collider> _entered = new();

        public bool IsColliding(Collider other)
        {
            return _entered.Contains(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
            if (!_entered.Contains(other))
                _entered.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
            if (_entered.Contains(other))
                _entered.Remove(other);
        }
    }
}