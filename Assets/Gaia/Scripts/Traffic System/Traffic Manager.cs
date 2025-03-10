using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{

    public class TrafficManager : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _roadInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _roadOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _pedestrianInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _pedestrianOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _bikeInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _bikeOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _transportInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _transportOutputWaypoints = new List<Waypoint>();

        [SerializeField] private GameObject _pedestrianPool;
        [SerializeField] private GameObject _roadPool;
        [SerializeField] private GameObject _bikePool;
        [SerializeField] private GameObject _transportPool;

        [SerializeField, Range(0.0f, 1.0f)] private float _pedestrianRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _roadRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _bikeRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _transportRatio;


    }

}