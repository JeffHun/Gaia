using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{

    public class TrafficManager : MonoBehaviour
    {
        // Waypoints inputs for all circuits
        [SerializeField] private List<Waypoint> _roadInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _pedestrianInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _bikeInputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _transportInputWaypoints = new List<Waypoint>();

        // Waypoints outputs for all circuits
        [SerializeField] private List<Waypoint> _roadOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _pedestrianOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _bikeOutputWaypoints = new List<Waypoint>();
        [SerializeField] private List<Waypoint> _transportOutputWaypoints = new List<Waypoint>();

        // Pools of assets to use
        [SerializeField] private GameObject _pedestrianPool;
        [SerializeField] private GameObject _roadPool;
        [SerializeField] private GameObject _bikePool;
        [SerializeField] private GameObject _transportPool;

        // Buffers to put assets used
        [SerializeField] private GameObject _pedestrianQueue;
        [SerializeField] private GameObject _roadQueue;
        [SerializeField] private GameObject _bikeQueue;
        [SerializeField] private GameObject _transportQueue;

        // Frequency ratios
        [SerializeField, Range(0.0f, 1.0f)] private float _pedestrianRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _roadRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _bikeRatio;
        [SerializeField, Range(0.0f, 1.0f)] private float _transportRatio;

        private void Start()
        {
            InvokeRepeating("SpawnTest", 3f, 3f);
        }

        private void Update()
        {
        }

        void SpawnTest()
        {
            if (_roadPool.transform.childCount > 0)
            {
                // pick a vehicle in the pool
                GameObject vehicle = _roadPool.transform.GetChild(Random.Range(0, _roadPool.transform.childCount - 1)).gameObject;
                vehicle.transform.SetParent(_roadQueue.transform);
                WaypointNavigator navigator = vehicle.GetComponent<WaypointNavigator>();
                navigator.enabled = true;

                // choose an input waypoint to move it there
                Waypoint targetWaypoint = _roadInputWaypoints[Random.Range(0, _roadInputWaypoints.Count)];


                // allow it to move
                vehicle.transform.position = targetWaypoint.GetPosition();
                vehicle.transform.forward = targetWaypoint.transform.forward;
                navigator.EnterOnCircuit(targetWaypoint);
            }
        }

        public void Despawn(Collider other)
        {
            GameObject otherVehicle = other.gameObject;
            otherVehicle.transform.SetParent(_roadPool.transform);
            otherVehicle.transform.position = _roadPool.transform.position;
            WaypointNavigator navigator = otherVehicle.GetComponent<WaypointNavigator>();
            navigator.ExitCircuit();
        }
    }

}