using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class CrosswalkWaypoint : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _waypoints = new List<Waypoint>(); // List of waypoints on the path
        [SerializeField] private Collider _crossingCollider;   //Trigger Collider to keep track of crossing objects
        [SerializeField] private int _areCrossing;     //Number of pedestrians on the crosswalk

        public List<Waypoint> Waypoints { get { return _waypoints; } set { _waypoints = value; } }
        public Collider CrossingCollider { get { return _crossingCollider;} set { _crossingCollider = value; } }
        public int AreCrossing { get { return _areCrossing; } }


        // Check if "other" is a Pedestrian or not
        private void OnTriggerEnter(Collider other)
        {
            _areCrossing++;
        }

        private void OnTriggerExit(Collider other)
        {
            _areCrossing--;
        }
    }
}