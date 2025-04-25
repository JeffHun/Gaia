using System.Collections.Generic;
using UnityEngine;


namespace Traffic
{

    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Waypoint _previousWaypoint;
        [SerializeField] private Waypoint _nextWaypoint;

        [Range(0f, 7f)]
        [SerializeField] private float _width = 1f;

        [SerializeField] private List<Waypoint> _branches = new List<Waypoint>();

        [Range(0f, 1f)]
        [SerializeField] private float _branchRatio = 0.5f;

        [SerializeField] private bool _canCross = true;

        public Waypoint PreviousWaypoint { get => _previousWaypoint; set => _previousWaypoint = value; }
        public Waypoint NextWaypoint { get => _nextWaypoint; set => _nextWaypoint = value; }
        public float Width { get => _width; set => _width = value; }
        public List<Waypoint> Branches { get => _branches; set => _branches = value; }
        public float BranchRatio { get => _branchRatio; set => _branchRatio = value; }
        public bool CanCross { get => _canCross; set => _canCross = value; }

        public Vector3 GetPosition()
        {
            Vector3 minBound = transform.position + transform.right * Width / 2f;
            Vector3 maxBound = transform.position - transform.right * Width / 2f;

            return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
        }
    }

}