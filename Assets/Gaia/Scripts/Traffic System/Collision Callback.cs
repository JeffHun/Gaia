using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent _collisionEvent = new UnityEvent();

    public UnityEvent CollisionEvent { get => _collisionEvent; set => _collisionEvent = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Vehicle" ||
            other.transform.tag == "Bike" ||
            other.transform.tag == "Transport")
        {
            CollisionEvent.Invoke();
        }
    }
}
