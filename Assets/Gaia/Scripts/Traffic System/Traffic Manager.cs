using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{

    public class TrafficManager : MonoBehaviour
    {
        [SerializeField] private List<StopWaypoint> _stops;
        [SerializeField] private List<CrosswalkWaypoint> _crosswalkWaypoints;
    }

}