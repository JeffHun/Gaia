using Components;
using System.Collections;
using System.Collections.Generic;
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
            _isTouchingFloor = true;
            StartCoroutine(Wait(_delay));
        }
    }

    private void Update()
    {
        if(_isLerping)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
            _timer += Time.deltaTime / 2f;
            transform.position = Vector3.Lerp(_startPos, this.GetComponent<ComponentData>().getAnchor().transform.position, _timer);
            transform.rotation = Quaternion.Lerp(_startRot, this.GetComponent<ComponentData>().getAnchor().transform.rotation, _timer);
            if(_timer >= 1)
            {
                _timer = 0;
                _isLerping = false;
                GetComponent<XRGrabInteractable>().enabled = true;
            }
        }

        if(transform.position.y <= -1f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = this.GetComponent<ComponentData>().getAnchor().transform.position;
            transform.rotation = this.GetComponent<ComponentData>().getAnchor().transform.rotation;
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
