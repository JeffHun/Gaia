using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class CrosswalkWaypoint : MonoBehaviour
    {
        [SerializeField] private Waypoint _waypoint; // List of waypoints on the path
        [SerializeField] private Collider _crossingCollider;   //Trigger Collider to keep track of crossing objects
        [SerializeField] private int _areCrossing;     //Number of pedestrians on the crosswalk

        public Waypoint Waypoint { get { return _waypoint; } set { _waypoint = value; } }
        public Collider CrossingCollider { get { return _crossingCollider;} set { _crossingCollider = value; } }
        public int AreCrossing { get { return _areCrossing; } }

        private void Update()
        {
            if (AreCrossing > 0)
            {
                Waypoint.CanCross = false;
            }
            else
            {
                Waypoint.CanCross = true;
            }
        }

        // Check if "other" is a Pedestrian or not
        private void OnTriggerEnter(Collider other)
        {
            if( other.tag == "Pedestrian")
            {
                _areCrossing++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Pedestrian")
            {
                _areCrossing--;
            }
        }
    }
}