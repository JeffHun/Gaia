using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class StopWaypoint : MonoBehaviour
    {
        private Waypoint _waypoint;
        private Collider _checkArea;
        private bool _isStopped;

        public Waypoint Waypoint { get { return _waypoint; } set { _waypoint = value; } }
        public Collider CheckArea { get { return _checkArea; } set { _checkArea = value; } }
        public bool IsStopped { get { return _isStopped; } }
    }
}