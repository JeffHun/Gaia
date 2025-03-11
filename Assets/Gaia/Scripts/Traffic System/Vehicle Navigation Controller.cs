using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class VehicleNavigationController : NavigationController
    {
        [SerializeField] private ParticleSystem _exhaustParticles;
        [SerializeField] private bool _parked = true;

        protected override void Awake()
        {
            base.Awake();
            _exhaustParticles = gameObject.transform.GetComponentInChildren<ParticleSystem>();
            if ( _exhaustParticles != null )
                _exhaustParticles.gameObject.SetActive(false);

        }

        public override void Activate()
        {
            base.Activate();
            _parked = false;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _parked = true;
        }

        protected override void Update()
        {
            base.Update();

            if (_parked)
            {
                _movementSpeed = 0f;
            }



            if (!_parked)
            {
                if (_exhaustParticles != null)
                    _exhaustParticles.gameObject.SetActive(true);
                _movementSpeed = _maxMovementSpeed;
            }
            else
            {
                if (_exhaustParticles != null)
                    _exhaustParticles.gameObject.SetActive(false);
                ReachedDestination = true;
            }
        }
    }
}