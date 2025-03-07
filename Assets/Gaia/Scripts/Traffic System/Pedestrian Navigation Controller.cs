using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class PedestrianNavigationController : NavigationController
    {
        public Animator animator;

        protected override void Update()
        {
            base.Update();
            if (animator != null )
            {
                animator.SetFloat("Velocity", GetVelocity());
            }
        }
    }
}