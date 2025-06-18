using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class BikeNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;
        [SerializeField, Range(0f, 1f)] private float _speedThreshold = 0.1f;

        override protected void Update()
        {
            base.Update();

            if (_animator != null)
            {

                if (_movementSpeed / _maxMovementSpeed >= _speedThreshold)
                {
                    _animator.SetFloat("Velocity", 1f);
                    _animator.speed = _movementSpeed / _maxMovementSpeed;
                }

                if (_movementSpeed < Mathf.Epsilon)
                {
                    _animator.SetFloat("Velocity", 0f);
                }
            }
        }
    }
}