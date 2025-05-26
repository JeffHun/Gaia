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
        [SerializeField] private Vector3 _shelfScale;
        [SerializeField] private Vector3 _handScale;
        [SerializeField] private Vector3 _carScale;

        public int ComponentID => _componentId;

        public Category Category => _category;

        public string Name => _name;

        public int Price => _price;

        public int ManufactureFootprint => _manufactureFootprint;

        public int UseFootprint => _useFootprint;

        public int RecycleFootprint => _recycleFootprint;

        public Sprite Image => _image;

        public GameObject Model => _model;

        public Vector3 ShelfScale => _shelfScale;

        public Vector3 HandScale => _handScale;

        public Vector3 CarScale => _carScale;
    }
}
