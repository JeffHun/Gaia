using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class PedestrianNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationWalkSpeed = 1.5f;

        [SerializeField] private float _waitingTime = 5f;
        [SerializeField] private float _waitingTimer = 0f;
        [SerializeField] private float _recoverTime = 2f;
        [SerializeField] private float _recoverTimer = 0f;
        private bool _hasToTurn = false;
        private Quaternion targetRotation = new Quaternion();

        protected override void Awake()
        {
            base.Awake();
            _waitingTimer = _waitingTime;
        }

        protected override void Update()
        {
            base.Update();
                        

            if (_animator != null )
            {
                _animator.SetFloat("Velocity", _movementSpeed / _animationWalkSpeed);
            }


            if(Physics.Raycast(_ray, out _hit, _breakDistance + 10) && _tags.Contains(_hit.transform.tag) && _movementSpeed <= Mathf.Epsilon)
            {
                _waitingTimer -= Time.deltaTime;

                if (_waitingTimer <= 0f)
                {
                    _breaking = false;
                    _hasToTurn = true;
                    _waitingTimer = _waitingTime; Vector3 direction = _destination - transform.position;
                    direction.Normalize();

                    int choice = Random.Range(0, 1);

                    if (choice == 0)
                        targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(new Vector3(0, 45f, 0));
                    else
                        targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(new Vector3(0, -45f, 0));
                }
                if (_hasToTurn)
                {
                    _recoverTimer -= Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _easing);
                }
                if (_recoverTimer <= 0f)
                {
                    _hasToTurn = false;
                    _recoverTimer = _recoverTime;
                }
            }else if (_movementSpeed <= Mathf.Epsilon)
            {
                _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _easing);
            }
        }
    }
}