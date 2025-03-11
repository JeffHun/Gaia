using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class DespawnWaypoint : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _collisionEvent = new UnityEvent<Collider>();
        [SerializeField] private Collider _collider;

        public UnityEvent<Collider> CollisionEvent { get => _collisionEvent; set => _collisionEvent = value; }
        public Collider Collider { get => _collider; set => _collider = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Vehicle")
            {
                CollisionEvent.Invoke(other);
            }
        }
    }
}