using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallback : MonoBehaviour
{
    private UnityEvent _collisionEvent = new UnityEvent();

    public UnityEvent CollisionEvent { get => _collisionEvent; set => _collisionEvent = value; }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Vehicle")
        {
            CollisionEvent.Invoke();
        }
    }
}
