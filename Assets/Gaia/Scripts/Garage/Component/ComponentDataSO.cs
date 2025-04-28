using categories;
using UnityEngine;

namespace categories
{
    public enum Category
    {
        Type,
        Moteur,
        Options
    }
}

namespace Components
{
    [CreateAssetMenu(menuName = "Component/Component Data")]
    public class ComponentDataSO : ScriptableObject
    {
        [SerializeField] private int _componentId;
        [SerializeField] private Category _category;
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private int _manufactureFootprint;
        [SerializeField] private int _useFootprint;
        [SerializeField] private int _recycleFootprint;
        [SerializeField] private Sprite _image;
        [SerializeField] private GameObject _model;
        [SerializeField] private Vector3 _shelfScale;      // When on shelf
        [SerializeField] private Vector3 _handScale;      // When in hands

        public int GetCompId()
        {
            return _componentId;
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

        public Sprite GetImage()
        {
            return _image;
        }

        public GameObject GetModel()
        {
            return _model;
        }

        public Vector3 GetShelfScale()
        {
            return _shelfScale;
        }

        public Vector3 GetHandScale()
        {
            return _handScale;
        }
    }
}
