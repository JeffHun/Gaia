using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using categories;

public class ComponentAnchor : MonoBehaviour
{
    [SerializeField] GameObject _componentTemplate;
    [SerializeField] ComponentDataSO _componentDataSo;
    string _layerType = "ComponentType";
    string _layerEngine = "ComponentEngine";
    string _layerOptions = "ComponentOptions";

    int _compId;
    GameObject _component;

    public int GetId()
    {
        return _compId;
    }

    public GameObject GetComponent()
    {
        return _component;
    }

    void Start()
    {
        _component = Instantiate(_componentTemplate, transform.position, transform.rotation);
        _component.GetComponent<ComponentData>().SetComponentDataSO(_componentDataSo);
        _component.GetComponent<ComponentData>().SetAnchor(this);
        _compId = _component.GetComponent<ComponentData>().GetId();

        if (_component.GetComponent<ComponentData>().GetCategory() == Category.Type)
            _component.layer = LayerMask.NameToLayer(_layerType);
        if (_component.GetComponent<ComponentData>().GetCategory() == Category.Moteur)
            _component.layer = LayerMask.NameToLayer(_layerEngine);
        if (_component.GetComponent<ComponentData>().GetCategory() == Category.Options)
            _component.layer = LayerMask.NameToLayer(_layerOptions);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.parent.gameObject.tag == "Component")
        {
            if(other.transform.parent.GetComponent<ComponentData>().GetId() == _compId)
            {
                other.transform.parent.gameObject.transform.position = transform.position;
                other.transform.parent.gameObject.transform.rotation = transform.rotation;
                other.transform.parent.gameObject.transform.parent = transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Component")
            other.transform.parent.gameObject.GetComponent<ComponentData>().SetCompStatus(ComponentStatus.Shelf);
    }
}
