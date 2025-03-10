using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class PedestrianNavigationController : NavigationController
    {
        [SerializeField] private Animator _animator;

        protected override void Update()
        {
            base.Update();
            if (_animator != null )
            {
                _animator.SetFloat("Velocity", GetVelocity());
            }
        }
    }
}