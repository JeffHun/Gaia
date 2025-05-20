using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UIStates;
using Components;
using categories;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor _leftInteractor;
    [SerializeField] private XRDirectInteractor _rightInteractor;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ComponentPage _componentPage;
    [SerializeField] private OverviewPage _overviewPage;
    [SerializeField] private CarMovement _car;
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

    public float GetTotalFootPrint()
    {
        return _components[0].GetManufactureFootprint() +
                    _components[0].GetUseFootprint() +
                    _components[0].GetRecycleFootprint() +
                    _components[1].GetManufactureFootprint() +
                    _components[1].GetUseFootprint() +
                    _components[1].GetRecycleFootprint() +
                    _components[2].GetManufactureFootprint() +
                    _components[2].GetUseFootprint() +
                    _components[2].GetRecycleFootprint(); ;
    }

    public void AddCarComponent(ComponentData comp)
    {
        Rigidbody body = comp.GetComponent<Rigidbody>();
        switch (comp.GetCategory())
        {
            case Category.Type:
                AssignSocket(_components[0], _socketType);
                break;
            case Category.Moteur:
                AssignSocket(_components[1], _socketEngine);
                break;
            case Category.Options:
                AssignSocket(_components[2], _socketSettings);
                break;
        }

        if (_components[0] != null && _components[1] != null && _components[2] != null)
        {
            _uiManager.ChangeState(UIState.overview);
            _overviewPage.UpdatePage(_components);
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
                if (_components[i].GetId() == comp.GetId())
                {
                    _components[i] = null;
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
            // Show idle page at the beginning only
            //_uiManager.ChangeState(UIState.idle);
            _leftComponent = null;
        }

        if (_rightComponent && !_leftComponent)
        {
            _componentPage.UpdateCurrentComponent(_rightComponent);
            _uiManager.ChangeState(UIState.component);
        }
        if (_rightInteractor.interactablesSelected.Count == 0 && _rightComponent)
        {
            // Show idle page at the beginning only
            //_uiManager.ChangeState(UIState.idle);
            _rightComponent = null;
        }

        if (_rightComponent && _leftComponent)
        {
            _uiManager.ChangeState(UIState.warning);
        }

        Transform[] sockets = { _socketType.transform, _socketEngine.transform, _socketSettings.transform };

        for (int i = 0; i < _components.Length && i < sockets.Length; i++)
        {
            if (_components[i] != null)
            {
                Transform compTransform = _components[i].transform;
                Rigidbody compRb = _components[i].transform.GetComponent<Rigidbody>();

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
