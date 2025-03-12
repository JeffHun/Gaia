using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class WaypointNavigator : MonoBehaviour
    {
        NavigationController controller;
        [SerializeField] private Waypoint _currentWaypoint;
        [SerializeField] private Waypoint _nextWaypoint;

        int _direction;

        private void Awake()
        {
            controller = GetComponent<NavigationController>();
        }

        private void Start()
        {
            _direction = Mathf.RoundToInt(Random.Range(0f, 1f));

            if (_nextWaypoint != null)
            {
                controller.SetDestination(_nextWaypoint.GetPosition());
            }
        }

        private void Update()
        {
            if (_nextWaypoint != null)
            {
                if (controller.ReachedDestination)
                {
                    bool shouldBranch = false;
                    _currentWaypoint = _nextWaypoint;

                    if (_currentWaypoint.CanCross)
                    {
                        if (_nextWaypoint.Branches != null && _nextWaypoint.Branches.Count > 0)
                        {
                            shouldBranch = Random.Range(0f, 1f) <= _nextWaypoint.BranchRatio ? true : false;
                        }

                        if (shouldBranch)
                        {
                            _nextWaypoint = _nextWaypoint.Branches[Random.Range(0, _nextWaypoint.Branches.Count)];
                            if (_direction == 0 && _nextWaypoint.NextWaypoint == null ||
                                _direction == 0 && _nextWaypoint.NextWaypoint != null &&
                                _nextWaypoint.NextWaypoint.BranchRatio == 1)
                            {
                                _direction = 1;
                            }
                            else
                            {
                                _direction = 0;
                            }

                            if (_direction == 1 && _nextWaypoint.PreviousWaypoint == null ||
                                _direction == 0 && _nextWaypoint.PreviousWaypoint != null &&
                                _nextWaypoint.PreviousWaypoint.BranchRatio == 1)
                            {
                                _direction = 0;
                            }
                            else
                            {
                                _direction = 1;
                            }
                        }
                        else
                        {
                            if (_direction == 0)
                            {
                                if (_nextWaypoint.NextWaypoint != null)
                                {
                                    _nextWaypoint = _nextWaypoint.NextWaypoint;
                                }
                                else
                                {
                                    _nextWaypoint = _nextWaypoint.PreviousWaypoint;
                                    _direction = 1;
                                }
                            }
                            else if (_direction == 1)
                            {
                                if (_nextWaypoint.PreviousWaypoint != null)
                                {
                                    _nextWaypoint = _nextWaypoint.PreviousWaypoint;
                                }
                                else
                                {
                                    _nextWaypoint = _nextWaypoint.NextWaypoint;
                                    _direction = 0;
                                }
                            }
                        }
                        if (_nextWaypoint != null)
                            controller.SetDestination(_nextWaypoint.GetPosition());
                    }
                }
            }
        }
    
        public void EnterOnCircuit(Waypoint waypoint)
        {
            _nextWaypoint = waypoint;
            controller = GetComponent<NavigationController>();
            controller.Activate();
            controller.SetDestination(_nextWaypoint.GetPosition());
            _direction = 0;
        }
    
        public void ExitCircuit()
        {
            _currentWaypoint = null;
            _nextWaypoint = null;
            controller.Deactivate();
        }

    }

}