using categories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Components
{
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
        Vector3 _resizedCollider;

        public void SetComponentDataSO(ComponentDataSO compDataSO)
        {
            _componentData = compDataSO;
            Init();
        }

        public void SetAnchor(ComponentAnchor anchor)
        {
            _anchor = anchor;
        }

        public ComponentAnchor getAnchor()
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
            _collider = GetComponent<BoxCollider>();
            _collider.size = _componentData.GetColliderScale();
            _resizedCollider = new Vector3(_collider.size.x* _componentData.GetStoredScale().x,
                    _collider.size.y* _componentData.GetStoredScale().y,
                    _collider.size.z* _componentData.GetStoredScale().z);

            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            if (_xrGrabInteractable)
            {
                _xrGrabInteractable.colliders.Add(GetComponentInChildren<Collider>());
            }
        }


        /*public ComponentData(int id, Category category, string name, int price, int manufactureFootprint, int useFootprint, int recycleFootprint, Sprite img)
        {
            _id = id;
            _category = category;
            _name = name;
            _price = price;
            _manufactureFootprint = manufactureFootprint;
            _useFootprint = useFootprint;
            _recycleFootprint = recycleFootprint;
            _img = img;
        }*/

        private void Update()
        {
            if (_xrGrabInteractable.isSelected)
            {
                transform.localScale = _componentData.GetStoredScale();
                _collider.size = _resizedCollider;
                transform.Find(_componentData.GetModel().name + "(Clone)").localScale = _componentData.GetStoredScale();
            }
            else
            {
                transform.localScale = Vector3.one;
                transform.Find(_componentData.GetModel().name + "(Clone)").localScale = Vector3.one;
                _collider.size = _componentData.GetColliderScale();
            }
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
    }
}