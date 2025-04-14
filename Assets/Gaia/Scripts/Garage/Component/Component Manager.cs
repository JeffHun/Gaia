using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UIStates;
using Components;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor _leftInteractor;
    [SerializeField] private XRDirectInteractor _rightInteractor;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ComponentPage _componentPage;
    private ComponentData _leftComponent;
    private ComponentData _rightComponent;

    // Update is called once per frame
    void Update()
    {
        if (_leftInteractor.interactablesSelected.Count >= 1)
            _leftComponent = _leftInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>();
        else
            _leftComponent = null;
        if (_rightInteractor.interactablesSelected.Count >= 1)
            _rightComponent = _rightInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>();
        else 
            _rightComponent = null;

        Debug.Log(_rightComponent);
        if (_leftComponent)
        {
            _uiManager.ChangeState(UIState.component);
            _componentPage.AddComponent(_leftComponent);
        }
        else if (_leftInteractor.interactablesSelected.Count == 0 && _leftComponent)
        {
            _uiManager.ChangeState(UIState.idle);
            _componentPage.RemoveComponent(_leftComponent);
        }

        if (_rightComponent)
        {
            _uiManager.ChangeState(UIState.component);
            _componentPage.AddComponent(_rightComponent);
        }
        else if (_rightInteractor.interactablesSelected.Count == 0 && _rightComponent)
        {
            _uiManager.ChangeState(UIState.idle);
            _componentPage.RemoveComponent(_rightComponent);
        }
    }
}
