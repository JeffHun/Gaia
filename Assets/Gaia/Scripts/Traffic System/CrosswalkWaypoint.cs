using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(BoxCollider))]
    public class CrosswalkWaypoint : MonoBehaviour
    {
        public List<Waypoint> Waypoint = new List<Waypoint>(); // List of waypoints on the path
        public Collider CrossingCollider;   //Trigger Collider to keep track of crossing objects
        public int AreCrossing;     //Number of pedestrians on the crosswalk


        // We will have to check wheter the "other" Object is a car or a pedestrian.
        // If it is a car, then Pedestrians will slow down or stop (We may be able to move this logic
        // to pedestrian navigation controller to tell them to slow down if they're seeing a car
        // moving in front of them
        private void OnTriggerEnter(Collider other)
        {
            AreCrossing++;
        }

        private void OnTriggerExit(Collider other)
        {
            AreCrossing--;
        }
    }
}