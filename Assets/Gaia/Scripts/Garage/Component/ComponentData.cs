using categories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

namespace Components
{
    public enum ComponentStatus
    {
        Use,
        Shelf,
        Hand
    }

    public class ComponentData : MonoBehaviour
    {
        [SerializeField] private ComponentDataSO _componentData;

        int _id;
        Category _category;
        string _name;
        int _price;
        int _manufactureFootprint;
        int _useFootprint;
        int _recycleFootprint;
        Sprite _img;
        ComponentAnchor _anchor;

        XRGrabInteractable _xrGrabInteractable;
        BoxCollider _collider;

        public void SetComponentDataSO(ComponentDataSO compDataSO)
        {
            _componentData = compDataSO;
            Init();
        }

        public void SetAnchor(ComponentAnchor anchor)
        {
            _anchor = anchor;
        }

        public ComponentAnchor GetAnchor()
        {
            return _anchor;
        }

        private void Init()
        {
            _id = _componentData.GetCompId();
            _category = _componentData.GetCategory();
            _name = _componentData.GetName();
            _price = _componentData.GetPrice();
            _manufactureFootprint = _componentData.GetManufactureFootprint();
            _useFootprint = _componentData.GetUseFootprint();
            _recycleFootprint = _componentData.GetRecycleFootprint();
            _img = _componentData.GetImage();
            Instantiate(_componentData.GetModel(), gameObject.transform, false);
            _collider = transform.GetComponent<BoxCollider>();
            _collider.center = transform.GetChild(0).GetComponent<MeshRenderer>().bounds.center - transform.position;
            _collider.size = transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size;

            _xrGrabInteractable = transform.GetComponent<XRGrabInteractable>();
            if (_xrGrabInteractable)
            {
                _xrGrabInteractable.colliders.Add(GetComponentInChildren<Collider>());
            }
        }

        public void SetCompStatus(ComponentStatus status)
        {
            if(status == ComponentStatus.Use)
            {
                transform.GetChild(0).localScale = _componentData.GetCarScale();
            }
            if (status == ComponentStatus.Hand)
            {
                transform.GetChild(0).localScale = _componentData.GetHandScale();
            }

            if (status == ComponentStatus.Shelf)
            {
                transform.GetChild(0).localScale = _componentData.GetShelfScale();
            }
        }

        public void SetCompStatusHand()
        {
            SetCompStatus(ComponentStatus.Hand);
        }

        public void OnBeginGrab()
        {
            SetCompStatus(ComponentStatus.Hand);
        }

        public int GetId()
        {
            return _id;
        }

        public Category GetCategory()
        {
            return _category;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetPrice()
        {
            return _price;
        }

        public int GetManufactureFootprint()
        {
            return _manufactureFootprint;
        }

        public int GetUseFootprint()
        {
            return _useFootprint;
        }

        public int GetRecycleFootprint()
        {
            return _recycleFootprint;
        }

        public Sprite GetImg()
        {
            return _img;
        }

        public GameObject GetModel()
        {
            return _componentData.GetModel();
        }

        public Vector3 GetShelfScale()
        {
            return _componentData.GetShelfScale();
        }

        public Vector3 GetHandScale()
        {
            return _componentData.GetHandScale();
        }

        public Vector3 GetCarScale()
        {
            return _componentData.GetCarScale();
        }
    }
}