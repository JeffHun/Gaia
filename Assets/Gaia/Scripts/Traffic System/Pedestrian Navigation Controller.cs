using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class PedestrianNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;
        private float _animationWalkSpeed = 1.5f;

        protected override void Update()
        {
            base.Update();
            if (_animator != null )
            {
                _animator.SetFloat("Velocity", _movementSpeed / _animationWalkSpeed);
            }
        }
    }
}