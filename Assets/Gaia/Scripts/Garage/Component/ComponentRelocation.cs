using Components;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComponentRelocation : MonoBehaviour
{
    [SerializeField] float _delay = 5f;
    bool _isTouchingFloor = false;
    bool _isLerping = false;
    Vector3 _startPos;
    Quaternion _startRot;
    float _timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            StartLerpingRoute(_delay);
        }
    }

    public void StartLerpingRoute(float delay)
    {
        _isTouchingFloor = true;
        StartCoroutine(Wait(delay));
    }

    private void Update()
    {
        if(_isLerping)
        {
            GetComponent<ComponentData>().SetCompStatus(ComponentStatus.Shelf);
            GetComponent<XRGrabInteractable>().enabled = false;
            _timer += Time.deltaTime / 2f;
            transform.position = Vector3.Lerp(_startPos, GetComponent<ComponentData>().GetAnchor().transform.position, _timer);
            transform.rotation = Quaternion.Lerp(_startRot, GetComponent<ComponentData>().GetAnchor().transform.rotation, _timer);
            if(_timer >= 1)
            {
                _timer = 0;
                _isLerping = false;
                GetComponent<XRGrabInteractable>().enabled = true;
                transform.parent = GetComponent<ComponentData>().GetAnchor().transform;
            }
        }

        if(transform.position.y <= -1f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = GetComponent<ComponentData>().GetAnchor().transform.position;
            transform.rotation = GetComponent<ComponentData>().GetAnchor().transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            _isTouchingFloor = false;
        }
    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_isTouchingFloor)
        {
            _startPos = transform.position;
            _startRot = transform.rotation;
            _isLerping = true;
        }
    }
}
