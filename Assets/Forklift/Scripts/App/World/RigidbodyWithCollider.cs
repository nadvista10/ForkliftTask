using UnityEngine;

namespace Forklift.App.World
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class RigidbodyWithCollider : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public Collider Collider  { get; private set; }

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
    }
}