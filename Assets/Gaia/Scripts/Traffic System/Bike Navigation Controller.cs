using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class BikeNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animMovingSpeed = 1f;

        override protected void Update()
        {
            base.Update();

            if (_animator != null)
            {
                _animator.SetFloat("Velocity", _movementSpeed / _animMovingSpeed);
            }


        }
    }
}