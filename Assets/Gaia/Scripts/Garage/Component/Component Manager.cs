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
    [SerializeField] private OverviewPage _overviewPage;
    [SerializeField] private ScatterPlotChart _resultPage;
    [SerializeField] private CarMovement _car;
    [SerializeField] private Socket _typeSocket;
    [SerializeField] private Socket _engineSocket;
    [SerializeField] private Socket _settingSocket;

    private ComponentData _leftComponent;
    private ComponentData _rightComponent;

    private ComponentData[] _components;

    public ComponentData TypeComponent { get => _components[0]; private set { _components[0] = value; } }
    public ComponentData EngineComponent { get => _components[1]; private set { _components[1] = value; } }
    public ComponentData SettingComponent { get => _components[2]; private set { _components[2] = value; } }

    public bool IsCarComplete => _components[0] != null && _components[1] != null && _components[2] != null;

    private void Awake()
    {
        _components = new ComponentData[3];
    }

    public float GetTotalFootprint()
    {
        return TypeComponent.GetComponentTotalFootprint() +
                EngineComponent.GetComponentTotalFootprint() +
                SettingComponent.GetComponentTotalFootprint();
    }

    public float GetTotalPrice()
    {
        return TypeComponent.Price + EngineComponent.Price + SettingComponent.Price;
    }

    public void AddCarComponent(ComponentData comp)
    {
        Rigidbody body = comp.GetComponent<Rigidbody>();
        switch (comp.Category)
        {
            case Category.Type:
                TypeComponent = comp;
                AssignSocket(TypeComponent, _typeSocket);
                break;
            case Category.Moteur:
                EngineComponent = comp;
                AssignSocket(EngineComponent, _engineSocket);
                break;
            case Category.Options:
                SettingComponent = comp;
                AssignSocket(SettingComponent, _settingSocket);
                break;
        }

        _overviewPage.UIAddComponent(comp);

        if (IsCarComplete)
        {
            _uiManager.ChangeState(UIState.result);
            _overviewPage.UpdatePage(_components);
            _resultPage.AddPoint(GetTotalFootprint(), GetTotalPrice());
        }
    }

    public void RemoveCarComponent(ComponentData comp)
    {
        Rigidbody body = comp.GetComponent<Rigidbody>();
        body.useGravity = true;
        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] != null)
            {
                if (_components[i].ID == comp.ID)
                {
                    _components[i] = null;
                    _overviewPage.UIRemoveComponent(comp);
                    return;
                }
            }
        }
    }

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
            _leftComponent = null;
        }

        if (_rightComponent && !_leftComponent)
        {
            _componentPage.UpdateCurrentComponent(_rightComponent);
            _uiManager.ChangeState(UIState.component);
        }
        if (_rightInteractor.interactablesSelected.Count == 0 && _rightComponent)
        {
            _rightComponent = null;
        }

        if (_rightComponent && _leftComponent)
        {
            _uiManager.ChangeState(UIState.warning);
        }

        Transform[] sockets = { _typeSocket.transform, _engineSocket.transform, _settingSocket.transform };

        for (int i = 0; i < _components.Length && i < sockets.Length; i++)
        {
            if (_components[i] != null)
            {
                Transform compTransform = _components[i].transform;
                Rigidbody compRb = _components[i].transform.GetComponent<Rigidbody>();

                if(!_rightComponent && !_leftComponent)
                    _components[i].SetCompStatus(ComponentStatus.Use);

                compTransform.position = sockets[i].position;
                compTransform.rotation = sockets[i].rotation;

                if (compRb != null)
                {
                    compRb.velocity = Vector3.zero;
                    compRb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    public void OnColliderEntered(Collider collider)
    {
        ComponentData comp = collider.transform.parent.GetComponent<ComponentData>();
        AddCarComponent(comp);
        _componentPage.UIAddComponent(comp);
    }

    public void OnColliderExited(Collider collider)
    {
        ComponentData comp = collider.transform.parent.GetComponent<ComponentData>();
        RemoveCarComponent(comp);
        _componentPage.UIRemoveComponent(comp);
    }

    public ComponentData[] GetComponents()
    {
        return _components;
    }

    private void AssignSocket(ComponentData comp, Socket socket)
    {
        comp.transform.position = socket.transform.position;
        comp.transform.rotation = socket.transform.rotation;
        comp.transform.parent = socket.transform;
    }
}
