using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class StopWaypoint : MonoBehaviour
    {
        public Waypoint Waypoint;
        public Collider CheckArea;
        public bool IsStopped;
    }
}