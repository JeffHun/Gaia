using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationController : MonoBehaviour
{
    public float MovementSpeed = 1f;
    public float RotationSpeed = 120f;
    public float StopDistance = 2.5f;
    public Vector3 Destination = new Vector3(0f, 0f, 0f);
    public bool ReachedDestination = false;

    private void Update()
    {
        if(!ReachedDestination)
        {
            MoveToDestination();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = destination;
        ReachedDestination = false;
    }

    public void MoveToDestination()
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
