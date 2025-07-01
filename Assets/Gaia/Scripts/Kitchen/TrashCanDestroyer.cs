using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashCanDestroyer : MonoBehaviour
{
    [Serializable]
    public class Meat
    {
        [SerializeField]
        public GameObject meat;
        [SerializeField]
        public float timer;
        [SerializeField]
        public Vector3 iniScale;

        public Meat(GameObject meat, Vector3 iniScale)
        {
            this.meat = meat;
            this.iniScale = iniScale;
            timer = 0;
        }
    }

    public float Speed;

    float _timer;
    bool _isDetected;
    [SerializeField]
    List<Meat> _meats = new List<Meat>();
    Vector3 _iniScale;
    Vector3 _targetScale = new Vector3(.001f, .001f, .001f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Veget") ||
            other.gameObject.CompareTag("Saumon") ||
            other.gameObject.CompareTag("Beef") ||
            other.gameObject.CompareTag("Porc") ||
            other.gameObject.CompareTag("Chicken"))
        {
            var meatObj = other.gameObject;
            _meats.Add(new Meat(meatObj, meatObj.transform.localScale));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Veget") ||
            other.gameObject.CompareTag("Saumon") ||
            other.gameObject.CompareTag("Beef") ||
            other.gameObject.CompareTag("Porc") ||
            other.gameObject.CompareTag("Chicken"))
        {
            var meatObj = other.gameObject;
            for (int i = _meats.Count - 1; i >= 0; i--)
            {
                if (_meats[0].meat == meatObj)
                {
                    _meats.Remove(_meats[0]);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        if (_meats.Count > 0)
        {
            for (int i = _meats.Count - 1; i >= 0; i--)
            {
                if(_meats[0].meat == null)
                {
                    _meats.Remove(_meats[0]);
                    continue;
                }
                _meats[0].timer += Time.deltaTime;
                _meats[0].meat.transform.localScale = Vector3.Lerp(_meats[0].iniScale, _targetScale, _meats[0].timer * Speed);
                if (_meats[0].meat.transform.localScale == _targetScale)
                {
                    XRGrabInteractable meatGrabInteractable = _meats[0].meat.GetComponent<XRGrabInteractable>();
                    if (meatGrabInteractable != null &&
                        meatGrabInteractable.interactorsSelecting.Count > 0)
                    {
                        var interactor = meatGrabInteractable.firstInteractorSelecting as XRBaseInteractor;
                        if (interactor)
                        {
                            interactor.EndManualInteraction();
                        }
                    }
                    if(_meats[0].meat)
                        Destroy(_meats[0].meat);
                    _meats.Remove(_meats[0]);
                }
            }
            _isDetected = false;
        }
    }
}