using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent _collisionEvent = new UnityEvent();
    [SerializeField] private UnityEvent<Collider> _collisionExit = new UnityEvent<Collider>();
    [SerializeField] private UnityEvent<Collider> _collisionEnter = new UnityEvent<Collider>();
    [SerializeField] private List<string> _tags = new List<string>();


    public UnityEvent CollisionEvent { get => _collisionEvent; set => _collisionEvent = value; }
    public UnityEvent<Collider> CollisionExit { get => _collisionExit; set => _collisionExit = value; }
    public UnityEvent<Collider> CollisionEnter { get => _collisionEnter; set => _collisionEnter = value; }

    private void OnTriggerStay(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionExit.Invoke(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Contains(other.transform.tag))
        {
            CollisionEnter.Invoke(other);
        }
    }
}
