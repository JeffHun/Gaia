using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Traffic
{
    public abstract class NavigationController : MonoBehaviour
    {
        [SerializeField] protected float _maxMovementSpeed = 1f;
        [SerializeField] protected float _movementSpeed = 0;
        [SerializeField] protected float _rotationSpeed = 120f;
        [SerializeField] protected float acceleration = 1f;
        [SerializeField] protected float _stopDistance = 2.5f;
        [SerializeField] protected float _reachDistance = 1f;
        [SerializeField] protected Vector3 _destination = new Vector3(0f, 0f, 0f);
        [SerializeField] protected bool _reachedDestination = false;
        [SerializeField] protected AnimationCurve _curve;
        protected Vector3 _offset = Vector3.zero;
        protected Vector3 _previous;
        protected float _velocity;
        [SerializeField] protected float _breakDistance = 25f;
        [SerializeField] protected float defaultBreakForce = 1.2f;
        [SerializeField] protected float maxBreakForce = 1.2f;
        [SerializeField] protected float _breakForce = 1.2f;
        [SerializeField] protected List<string> _tags = new List<string>();
        protected Ray _ray;
        protected RaycastHit _hit;
        private Vector3 _rayOffset = new Vector3(0f, 0.5f, 0f);
        private float _lerpT;
        [SerializeField] protected float _easing;
        
        [SerializeField] protected bool _breaking = false;
        [SerializeField] protected bool _isAllowedToCross = true;

        public bool ReachedDestination { get => _reachedDestination; set => _reachedDestination = value; }
        public bool IsAllowedToCross { get => _isAllowedToCross; set => _isAllowedToCross = value; }

        public float GetVelocity()
        {
            return _velocity;
        }
        
        public virtual void Activate()
        {
            _lerpT = 0f;
            ReachedDestination = false;
            _offset.y = transform.position.y;
            _movementSpeed = _maxMovementSpeed;
            _breakForce = defaultBreakForce;
        }

        public virtual void Deactivate()
        {
            ReachedDestination = true;
            _movementSpeed = 0f;
        }
        protected virtual void Awake()
        {
            _lerpT = 0f;
            _offset.y = transform.position.y;
            _maxMovementSpeed += Random.Range(_maxMovementSpeed * -1, _maxMovementSpeed) / 3;
            _movementSpeed = _maxMovementSpeed;
            _breakForce = defaultBreakForce;
        }

        protected virtual void Update()
        {
            _easing = _curve.Evaluate(_lerpT);
            if (_breaking)
            {
                _lerpT += Time.deltaTime * _breakForce;
            }
            else
            {
                _lerpT += Time.deltaTime * acceleration;
            }


            _ray = new Ray(transform.position + _rayOffset, transform.forward);
            _velocity = ((transform.position - _previous).magnitude) / Time.deltaTime;
            _previous = transform.position;

            SpeedManagement();

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

            if (direction.magnitude < _reachDistance)
            {
                ReachedDestination = true;
                return;
            }

            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * _movementSpeed / 8 * Time.deltaTime);


            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }


        protected virtual void SpeedManagement()
        {

            if (Physics.Raycast(_ray, out _hit, _breakDistance + 10) && _tags.Contains(_hit.transform.tag))
            {
                float distance = Vector3.Distance(transform.position, _hit.transform.position);
                if (distance <= _stopDistance * 1.2 ||
                    Vector3.Distance(transform.position, _destination) <= _stopDistance && !_isAllowedToCross)
                {
                    _breakForce = Mathf.Lerp(maxBreakForce, defaultBreakForce, distance / _breakDistance);
                    if (_movementSpeed > Mathf.Epsilon)
                    {
                        _movementSpeed = Mathf.Lerp(_movementSpeed, 0, _easing);
                    }
                    else
                    {
                        _movementSpeed = 0f;
                    }
                }
                else if (distance <= _breakDistance || 
                    Vector3.Distance(transform.position, _destination) <= _breakDistance && !_isAllowedToCross)
                {
                    if(!_breaking) 
                    {
                        _breaking = true;
                        _lerpT = 0f;
                    }
                    _breakForce = Mathf.Lerp(maxBreakForce, defaultBreakForce, distance / _breakDistance);
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed / 4, _easing);
                }
                else
                {
                    if (_breaking)
                    {
                        _breaking = false;
                        _lerpT = 0f;
                        _breakForce = defaultBreakForce;
                    }
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _easing);
                }
            }
            else if (!_isAllowedToCross)
            {
                float distance = Vector3.Distance(_destination, transform.position);
                if (distance <= _stopDistance - 4)
                {
                    _breakForce = Mathf.Lerp(maxBreakForce, defaultBreakForce, distance / _breakDistance);
                    if (_movementSpeed > Mathf.Epsilon)
                    {
                        _movementSpeed = Mathf.Lerp(_movementSpeed, 0, _easing);
                    }
                    else
                    {
                        _movementSpeed = 0f;
                    }
                }
                else if (distance <= _breakDistance)
                {
                    if (!_breaking)
                    {
                        _breaking = true;
                        _lerpT = 0f;
                    }
                    _breakForce = Mathf.Lerp(maxBreakForce, defaultBreakForce, distance / _breakDistance);
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed / 3, _easing);
                }
                else
                {
                    if (_breaking)
                    {
                        _breaking = false;
                        _lerpT = 0f;
                        _breakForce = defaultBreakForce;
                    }
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _easing);
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