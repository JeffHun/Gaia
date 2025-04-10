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

        private void Awake()
        {
            _id = _componentData.GetCompId();
            _category = _componentData.GetCategory();
            _name = _componentData.GetName();
            _price = _componentData.GetPrice();
            _manufactureFootprint = _componentData.GetManufactureFootprint();
            _useFootprint = _componentData.GetUseFootprint();
            _recycleFootprint = _componentData.GetRecycleFootprint();
            _img = _componentData.GetImage();
            Instantiate(_componentData.GetModel(), gameObject.transform);

            XRGrabInteractable xrGrabInteractable = GetComponent<XRGrabInteractable>();
            if (xrGrabInteractable)
            {
                xrGrabInteractable.colliders.Add(GetComponentInChildren<Collider>());
            }
            /*
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            if (meshCollider)
            {
                meshCollider.sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
            }*/

        }


        public ComponentData(int id, Category category, string name, int price, int manufactureFootprint, int useFootprint, int recycleFootprint, Sprite img)
        {
            _id = id;
            _category = category;
            _name = name;
            _price = price;
            _manufactureFootprint = manufactureFootprint;
            _useFootprint = useFootprint;
            _recycleFootprint = recycleFootprint;
            _img = img;
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