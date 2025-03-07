using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public abstract class NavigationController : MonoBehaviour
    {
        public float MovementSpeed = 1f;
        public float RotationSpeed = 120f;
        public float StopDistance = 2.5f;
        public Vector3 Destination = new Vector3(0f, 0f, 0f);
        public bool ReachedDestination = false;
        [SerializeField]
        private Vector3 offset = Vector3.zero;
        private Vector3 previous;
        private float velocity;

        public float GetVelocity()
        {
            return velocity;
        }
        

        protected virtual void Awake()
        {
            offset.y = transform.position.y;
        }

        protected virtual void Update()
        {
            velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
            previous = transform.position;

            if (!ReachedDestination)
            {
                MoveToDestination();
            }
        }

        public virtual void SetDestination(Vector3 destination)
        {
            Destination = destination + offset;
            ReachedDestination = false;
        }

        protected virtual void MoveToDestination()
        {
            Vector3 direction = Destination - transform.position;

            if (direction.magnitude < StopDistance)
            {
                ReachedDestination = true;
                return;
            }

            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        }
    }
}