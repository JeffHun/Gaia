using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CollisionCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent _collisionStayEvent = new UnityEvent();
    [SerializeField] private UnityEvent _collisionEnterEvent = new UnityEvent();
    [SerializeField] private List<string> _tags = new List<string>();


    public UnityEvent CollisionEvent { get => _collisionStayEvent; set => _collisionStayEvent = value; }
    public UnityEvent CollisionEnterEvent { get => _collisionEnterEvent; set => _collisionEnterEvent = value; }

    private void OnTriggerStay(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionEvent.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionEnterEvent.Invoke();
        }
    }
}
