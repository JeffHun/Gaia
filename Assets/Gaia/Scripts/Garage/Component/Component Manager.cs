using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UIStates;
using Components;
using categories;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor _leftInteractor;
    [SerializeField] private XRDirectInteractor _rightInteractor;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ComponentPage _componentPage;
    
    private ComponentData _leftComponent;
    private ComponentData _rightComponent;

    private ComponentData[] _components;

    private void Awake()
    {
        _components = new ComponentData[3];
    }

    public void AddCarComponent(ComponentData comp)
    {
        switch (comp.GetCategory())
        {
            case Category.Type:
                _components[0] = comp;
                break;
            case Category.Moteur:
                _components[1] = comp;
                break;
            case Category.Options:
                _components[2] = comp;
                break;
        }
    }


    public void RemoveCarComponent(ComponentData comp)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] != null)
            {
                if (_components[i].GetId() == comp.GetId())
                {
                    _components[i] = null;
                    return;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_leftInteractor.interactablesSelected.Count >= 1)
            _leftComponent = _leftInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>();
        if (_rightInteractor.interactablesSelected.Count >= 1)
            _rightComponent = _rightInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>();

        if (_leftComponent && !_rightComponent)
        {
            _uiManager.ChangeState(UIState.component);
            _componentPage.UpdateCurrentComponent(_leftComponent);
        }
        if (_leftInteractor.interactablesSelected.Count == 0 && _leftComponent)
        {
            _uiManager.ChangeState(UIState.idle);
            _leftComponent = null;
        }

        if (_rightComponent && !_leftComponent)
        {
            _uiManager.ChangeState(UIState.component);
            _componentPage.UpdateCurrentComponent(_rightComponent);
        }
        if (_rightInteractor.interactablesSelected.Count == 0 && _rightComponent)
        {
            _uiManager.ChangeState(UIState.idle);
            _rightComponent = null;
        }

        if(_rightComponent && _leftComponent)
        {
            _uiManager.ChangeState(UIState.warning);
        }
    }


    public void OnColliderEntered(Collider collider)
    {
        ComponentData comp = collider.GetComponent<ComponentData>();
        AddCarComponent(comp);
        _componentPage.UIAddComponent(comp);
    }

    public void OnColliderExited(Collider collider)
    {
        ComponentData comp = collider.GetComponent<ComponentData>();
        RemoveCarComponent(comp);
        _componentPage.UIRemoveComponent(comp);
    }
}
