using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    NavigationController controller;
    public Waypoint currentWaypoint;

    int direction;

    private void Awake()
    {
        controller = GetComponent<NavigationController>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (controller.ReachedDestination)
        {
            bool shouldBranch = false;

            if (currentWaypoint.Branches != null && currentWaypoint.Branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.BranchRatio ? true : false;
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.Branches[Random.Range(0, currentWaypoint.Branches.Count - 1)];
                if(direction == 0 && currentWaypoint.NextWaypoint.Branches != null && currentWaypoint.NextWaypoint.Branches.Count > 0)
                {
                    direction = 1;
                }
                if (direction == 1 && currentWaypoint.PreviousWaypoint.Branches != null && currentWaypoint.PreviousWaypoint.Branches.Count > 0)
                {
                    direction = 0;
                }
            }
            else
            {
                if (direction == 0)
                {
                    if (currentWaypoint.NextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.PreviousWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if (currentWaypoint.PreviousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.PreviousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                        direction = 0;
                    }
                }
            }

            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
