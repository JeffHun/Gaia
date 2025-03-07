using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class WaypointNavigator : MonoBehaviour
    {
        NavigationController controller;
        public Waypoint currentWaypoint;
        public Waypoint nextWaypoint;

        int direction;

        private void Awake()
        {
            controller = GetComponent<NavigationController>();
        }

        private void Start()
        {
            direction = Mathf.RoundToInt(Random.Range(0f, 1f));

            controller.SetDestination(nextWaypoint.GetPosition());
        }

        private void Update()
        {
            if (controller.ReachedDestination)
            {
                bool shouldBranch = false;
                currentWaypoint = nextWaypoint;

                if (currentWaypoint.CanCross)
                {
                    if (nextWaypoint.Branches != null && nextWaypoint.Branches.Count > 0)
                    {
                        shouldBranch = Random.Range(0f, 1f) <= nextWaypoint.BranchRatio ? true : false;
                    }

                    if (shouldBranch)
                    {
                        nextWaypoint = nextWaypoint.Branches[Random.Range(0, nextWaypoint.Branches.Count - 1)];
                        if (direction == 0 && nextWaypoint.NextWaypoint.Branches != null && nextWaypoint.NextWaypoint.Branches.Count > 0)
                        {
                            direction = 1;
                        }
                        if (direction == 1 && nextWaypoint.PreviousWaypoint.Branches != null && nextWaypoint.PreviousWaypoint.Branches.Count > 0)
                        {
                            direction = 0;
                        }
                    }
                    else
                    {
                        if (direction == 0)
                        {
                            if (nextWaypoint.NextWaypoint != null)
                            {
                                nextWaypoint = nextWaypoint.NextWaypoint;
                            }
                            else
                            {
                                nextWaypoint = nextWaypoint.PreviousWaypoint;
                                direction = 1;
                            }
                        }
                        else if (direction == 1)
                        {
                            if (nextWaypoint.PreviousWaypoint != null)
                            {
                                nextWaypoint = nextWaypoint.PreviousWaypoint;
                            }
                            else
                            {
                                nextWaypoint = nextWaypoint.NextWaypoint;
                                direction = 0;
                            }
                        }
                    }
                    controller.SetDestination(nextWaypoint.GetPosition());
                }
            }
        }
    }

}