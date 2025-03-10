using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class StopWaypoint : MonoBehaviour
    {
        [SerializeField] private Waypoint _waypoint;
        [SerializeField] private List<Collider> _checkAreas = new List<Collider>();
        private bool _hasSpace;
        private bool _comingVehicle;

        public Waypoint Waypoint { get { return _waypoint; } set { _waypoint = value; } }

        public List<Collider> CheckAreas { get => _checkAreas; set => _checkAreas = value; }

        private void Update()
        {
            if (!_comingVehicle && _hasSpace)
            {
                Waypoint.CanCross = true;
            }
            else
            {
                Waypoint.CanCross = false;
            }

            _comingVehicle = false;
            _hasSpace = true;
        }

        public void OnComingVehicle()
        {
            _comingVehicle = true;
        }

        public void OnFreeSpace() 
        {
            _hasSpace = false;
        }


        private void OnCollision(Collider other)
        {
            if (other.tag == "Vehicle")
            {

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Vehicle")
            {

            }
        }
    }
}