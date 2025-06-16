using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{

    public class TrafficManager : DynamicEnvItem
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

        // Spawn rate
        [SerializeField] private Vector2 _spawnRate = new Vector2(0f, 3f);
        [SerializeField] private Vector2 _transportSpawnRate = new Vector2(3f, 5f);

        private float _randTimer = 0f;
        private float _transportTimer = 0f;

        private void Start()
        {
        }

        private void Update()
        {
            if(_randTimer <= 0f)
            {
                _randTimer = Random.Range(_spawnRate.x, _spawnRate.y);
                float doSpawn = Random.Range(0f, 1f);

                if (doSpawn < _pedestrianRatio)
                {
                    Spawn(_pedestrianPool, _pedestrianQueue, _pedestrianInputWaypoints);
                }
                if (doSpawn < _roadRatio)
                {
                    Spawn(_roadPool, _roadQueue, _roadInputWaypoints);
                }
                if (doSpawn < _bikeRatio)
                {
                    Spawn(_bikePool, _bikeQueue, _bikeInputWaypoints);
                }
                if (doSpawn < _transportRatio)
                {
                    if (_transportTimer <= 0f)
                    {
                        _transportTimer = Random.Range(_transportSpawnRate.x, _transportSpawnRate.y);
                        Spawn(_transportPool, _transportQueue, _transportInputWaypoints);
                    }
                }

            }

            _randTimer -= Time.deltaTime;
            _transportTimer -= Time.deltaTime;
        }

        void Spawn(GameObject pool, GameObject queue, List<Waypoint> inputs)
        {
            if (pool.transform.childCount > 0)
            {
                // pick an entity in the pool
                GameObject entity = pool.transform.GetChild(Random.Range(0, pool.transform.childCount - 1)).gameObject;
                entity.transform.SetParent(queue.transform);
                WaypointNavigator navigator = entity.GetComponent<WaypointNavigator>();
                navigator.enabled = true;

                // choose an input waypoint to move it there
                Waypoint targetWaypoint = inputs[Random.Range(0, inputs.Count)];


                // allow it to move
                entity.transform.position = targetWaypoint.GetPosition();
                entity.transform.forward = targetWaypoint.transform.forward;
                navigator.EnterOnCircuit(targetWaypoint);
            }
        }

        public void Despawn(Collider other)
        {
            GameObject otherVehicle = other.gameObject;
            WaypointNavigator navigator = otherVehicle.GetComponent<WaypointNavigator>();

            switch (otherVehicle.tag)
            {
                case "Vehicle":
                    otherVehicle.transform.SetParent(_roadPool.transform);
                    otherVehicle.transform.position = _roadPool.transform.position;
                    navigator.ExitCircuit();
                    break;
                case "Bike":
                    otherVehicle.transform.SetParent(_bikePool.transform);
                    otherVehicle.transform.position = _bikePool.transform.position;
                    navigator.ExitCircuit();
                    break;
                case "Pedestrian":
                    otherVehicle.transform.SetParent(_pedestrianPool.transform);
                    otherVehicle.transform.position = _pedestrianPool.transform.position;
                    navigator.ExitCircuit();
                    break;
                case "Transport":
                    otherVehicle.transform.SetParent(_transportPool.transform);
                    otherVehicle.transform.position = _transportPool.transform.position;
                    navigator.ExitCircuit();
                    break;
                default:
                    return;
            }

        }

        public override void ChangeLook(Scenario scenario)
        {
            switch (scenario)
            {
                case Scenario.scenarioA:
                    break;
                case Scenario.scenarioB:
                    break;
                case Scenario.scenarioC:
                    break;
                case Scenario.scenarioD:
                    break;
                case Scenario.scenarioE:
                    break;
            }
        }
    }

}