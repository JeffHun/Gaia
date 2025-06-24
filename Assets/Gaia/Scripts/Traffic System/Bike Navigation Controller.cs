using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class BikeNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;
        [SerializeField, Range(0f, 1f)] private float _speedThreshold = 0.1f;
        private bool _isStopping = true;

        override protected void Update()
        {
            base.Update();

            if (_animator != null)
            {

                if (_movementSpeed / _maxMovementSpeed >= _speedThreshold)
                {
                    _animator.SetFloat("Velocity", 1f);
                    _animator.speed = _movementSpeed / _maxMovementSpeed;
                    _isStopping = false;
                }
                else if (_isStopping)
                {
                    _animator.SetFloat("Velocity", 0f);
                }
                else if (_movementSpeed < Mathf.Epsilon)
                {
                    _movementSpeed = 0;
                    StartCoroutine(Delay(1f));
                }


                if (_movementSpeed == 0)
                {
                    _movementSpeed = Mathf.Lerp(_movementSpeed, _maxMovementSpeed, _easing);
                }
            }
        }


        IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isStopping = true;
        }
    }
}