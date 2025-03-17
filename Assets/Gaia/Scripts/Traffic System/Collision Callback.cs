using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent _collisionEvent = new UnityEvent();
    [SerializeField] private List<string> _tags = new List<string>();


    public UnityEvent CollisionEvent { get => _collisionEvent; set => _collisionEvent = value; }

    private void OnTriggerStay(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionEvent.Invoke();
        }
    }
}
