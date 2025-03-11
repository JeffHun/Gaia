using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Traffic
{
    public abstract class NavigationController : MonoBehaviour
    {
        [SerializeField] protected float _maxMovementSpeed = 1f;
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected float _rotationSpeed = 120f;
        [SerializeField] protected float _stopDistance = 2.5f;
        [SerializeField] protected Vector3 _destination = new Vector3(0f, 0f, 0f);
        [SerializeField] protected bool _reachedDestination = false;
        [SerializeField]
        protected Vector3 _offset = Vector3.zero;
        protected Vector3 _previous;
        protected float _velocity;
        [SerializeField] protected float _breakDistance = 25f;
        [SerializeField] protected float _breakForce = 1.2f;
        private Ray _ray;
        private RaycastHit _hit;

        public bool ReachedDestination { get => _reachedDestination; set => _reachedDestination = value; }

        public float GetVelocity()
        {
            return _velocity;
        }
        
        public virtual void Activate()
        {
            ReachedDestination = false;
            _offset.y = transform.position.y;
            _movementSpeed = _maxMovementSpeed;
        }

        public virtual void Deactivate()
        {
            ReachedDestination = true;
            _movementSpeed = 0f;
        }
        protected virtual void Awake()
        {
            _offset.y = transform.position.y;
            _movementSpeed = _maxMovementSpeed;
        }

        protected virtual void Update()
        {
            _ray = new Ray(transform.position, transform.forward);
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

            // We check if there's something blocking the way
            RaycastAction();

            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }


        void RaycastAction()
        {
            if (Physics.Raycast(_ray, out _hit, 40))
            {
                if (_hit.transform.tag == "Vehicle" || _hit.transform.tag == "Pedestrian")
                {
                    float distance = Vector3.Distance(transform.position, _hit.transform.position);
                    if (distance <= _stopDistance)
                    {
                        _movementSpeed = 0f;
                    }
                    else if (distance <= _breakDistance)
                    {
                        _movementSpeed /= _breakForce;
                    }
                    else
                    {
                        _movementSpeed = _maxMovementSpeed;
                    }
                }
            }
        }
    }
}