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
    [SerializeField] private Socket _socketType;
    [SerializeField] private Socket _socketEngine;
    [SerializeField] private Socket _socketSettings;

    private ComponentData _leftComponent;
    private ComponentData _rightComponent;

    private ComponentData[] _components;

    private void Awake()
    {
        _components = new ComponentData[3];
    }

    public void AddCarComponent(ComponentData comp)
    {
        Rigidbody body = comp.GetComponent<Rigidbody>();
        switch (comp.GetCategory())
        {
            case Category.Type:
                _components[0] = comp;
                comp.transform.position = _socketType.transform.position;
                comp.transform.rotation = _socketType.transform.rotation;
                break;
            case Category.Moteur:
                _components[1] = comp;
                comp.transform.position = _socketEngine.transform.position;
                comp.transform.rotation = _socketEngine.transform.rotation;
                break;
            case Category.Options:
                _components[2] = comp;
                comp.transform.position = _socketSettings.transform.position;
                comp.transform.rotation = _socketSettings.transform.rotation;
                break;
        }
        body.useGravity = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }


    public void RemoveCarComponent(ComponentData comp)
    {
        Rigidbody body = comp.GetComponent<Rigidbody>();
        body.useGravity = true;
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
            _componentPage.UpdateCurrentComponent(_leftComponent);
            _uiManager.ChangeState(UIState.component);
        }
        if (_leftInteractor.interactablesSelected.Count == 0 && _leftComponent)
        {
            _uiManager.ChangeState(UIState.idle);
            _leftComponent = null;
        }

        if (_rightComponent && !_leftComponent)
        {
            _componentPage.UpdateCurrentComponent(_rightComponent);
            _uiManager.ChangeState(UIState.component);
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
