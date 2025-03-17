using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Traffic
{
    public abstract class NavigationController : MonoBehaviour
    {
        [SerializeField] protected float _maxMovementSpeed = 1f;
        [SerializeField] protected float _movementSpeed = 0;
        [SerializeField] protected float _acceleration = 1f;
        [SerializeField] protected float _rotationSpeed = 120f;
        [SerializeField] protected float _stopDistance = 2.5f;
        [SerializeField] protected float _reachDistance = 1f;
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
        private Vector3 _rayOffset = new Vector3(0f, 0.5f, 0f);

        public bool ReachedDestination { get => _reachedDestination; set => _reachedDestination = value; }

        public float GetVelocity()
        {
            return _velocity;
        }
        
        public virtual void Activate()
        {
            ReachedDestination = false;
            _offset.y = transform.position.y;
            Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _acceleration * Time.deltaTime);
        }

        public virtual void Deactivate()
        {
            ReachedDestination = true;
            _movementSpeed = 0f;
        }
        protected virtual void Awake()
        {
            _offset.y = transform.position.y;
            _maxMovementSpeed += Random.Range(_maxMovementSpeed * -1, _maxMovementSpeed) / 3;
            Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _acceleration * Time.deltaTime);
        }

        protected virtual void Update()
        {
            _ray = new Ray(transform.position + _rayOffset, transform.forward);
            _velocity = ((transform.position - _previous).magnitude) / Time.deltaTime;
            _previous = transform.position;

            if (!ReachedDestination)
            {
                MoveToDestination();
            }
            else
            {
                Mathf.Lerp(_movementSpeed, 0, _breakForce * Time.deltaTime);
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

            if (direction.magnitude < _reachDistance)
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
            if (Physics.Raycast(_ray, out _hit, _breakDistance + 10))
            {
                if (_hit.transform.tag == "Vehicle" || _hit.transform.tag == "Pedestrian" || 
                    _hit.transform.tag == "Transport" ||_hit.transform.tag == "Bike")
                {
                    float distance = Vector3.Distance(transform.position, _hit.transform.position);
                    if (distance <= _stopDistance)
                    {
                        Mathf.Lerp(_movementSpeed, 0, _breakForce * Time.deltaTime);
                    }
                    else if (distance <= _breakDistance)
                    {
                        if(_movementSpeed <= 0f)
                            Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _acceleration * Time.deltaTime);
                        _movementSpeed /= _breakForce;
                    }
                    else
                    {
                        Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _acceleration * Time.deltaTime);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_hit.point, 0.2f);
        }
    }
}