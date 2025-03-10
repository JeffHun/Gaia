using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public abstract class NavigationController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _rotationSpeed = 120f;
        [SerializeField] private float _stopDistance = 2.5f;
        [SerializeField] private Vector3 _destination = new Vector3(0f, 0f, 0f);
        [SerializeField] private bool _reachedDestination = false;
        [SerializeField]
        protected Vector3 _offset = Vector3.zero;
        protected Vector3 _previous;
        protected float _velocity;

        public bool ReachedDestination { get => _reachedDestination; set => _reachedDestination = value; }

        public float GetVelocity()
        {
            return _velocity;
        }
        

        protected virtual void Awake()
        {
            _offset.y = transform.position.y;
        }

        protected virtual void Update()
        {
            _velocity = ((transform.position - _previous).magnitude) / Time.deltaTime;
            _previous = transform.position;

            if (!ReachedDestination)
            {
                MoveToDestination();
            }
        }

        public virtual void SetDestination(Vector3 destination)
        {
            _destination = destination + _offset;
            ReachedDestination = false;
        }

        protected virtual void MoveToDestination()
        {
            Vector3 direction = _destination - transform.position;

            if (direction.magnitude < _stopDistance)
            {
                ReachedDestination = true;
                return;
            }

            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }
    }
}