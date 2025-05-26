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

        public int ID { get; private set; }
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int ManufactureFootprint { get; private set; }
        public int UseFootprint { get; private set; }
        public int RecycleFootprint { get; private set; }
        public Sprite ImageSprite { get; private set; }
        public ComponentAnchor Anchor { get; private set; }
        public Transform _modelTransform { get; private set; }

        private XRGrabInteractable _xrGrabInteractable;
        private BoxCollider _collider;

        public void SetComponentDataSO(ComponentDataSO compDataSO)
        {
            _componentData = compDataSO;
            Init();
        }

        public void SetAnchor(ComponentAnchor anchor)
        {
            Anchor = anchor;
        }

        public ComponentAnchor GetAnchor()
        {
            return Anchor;
        }

        private void Init()
        {
            ID = _componentData.ComponentID;
            Category = _componentData.Category;
            Name = _componentData.Name;
            Price = _componentData.Price;
            ManufactureFootprint = _componentData.ManufactureFootprint;
            UseFootprint = _componentData.UseFootprint;
            RecycleFootprint = _componentData.RecycleFootprint;
            ImageSprite = _componentData.Image;
            Instantiate(_componentData.Model, gameObject.transform, false);
            _modelTransform = transform.GetChild(0);
            _modelTransform.localScale = _componentData.ShelfScale;
            _collider = _modelTransform.GetComponent<BoxCollider>();

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
                _modelTransform.localScale = _componentData.CarScale;
            }
            if (status == ComponentStatus.Hand)
            {
                _modelTransform.localScale = _componentData.HandScale;
            }

            if (status == ComponentStatus.Shelf)
            {
                _modelTransform.localScale = _componentData.ShelfScale;
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

        public int GetComponentTotalFootprint()
        {
            return ManufactureFootprint + UseFootprint + RecycleFootprint;
        }

        public GameObject GetModel()
        {
            return _componentData.Model;
        }

        public Vector3 GetShelfScale()
        {
            return _componentData.ShelfScale;
        }

        public Vector3 GetHandScale()
        {
            return _componentData.HandScale;
        }

        public Vector3 GetCarScale()
        {
            return _componentData.CarScale;
        }
    }
}