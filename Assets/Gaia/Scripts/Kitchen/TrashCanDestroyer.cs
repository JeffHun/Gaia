using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanDestroyer : MonoBehaviour
{
    public float Speed;

    float _timer;
    bool _isDetected;
    GameObject _meat;
    Vector3 _iniScale;
    Vector3 _targetScale = new Vector3(.001f, .001f, .001f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Veget") || other.gameObject.CompareTag("Saumon") || other.gameObject.CompareTag("Beef") || other.gameObject.CompareTag("Porc") || other.gameObject.CompareTag("Chicken"))
        {
            _meat = other.gameObject;
            _iniScale = _meat.transform.localScale;
            _meat.GetComponent<Rigidbody>().isKinematic = true;
            _isDetected = true;
        }
    }

    private void Update()
    {
        if (_isDetected)
        {
            _timer += Time.deltaTime;
            _meat.transform.localScale = Vector3.Lerp(_iniScale, _targetScale, _timer * Speed);
            if (_meat.transform.localScale == _targetScale)
            {
                Destroy(_meat);
                _timer = 0;
                _isDetected = false;
            }
        }
    }
}