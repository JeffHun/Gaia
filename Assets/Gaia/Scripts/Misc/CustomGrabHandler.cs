using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;

public class CustomGrabHandler : MonoBehaviour
{
    public XRController xrController; // Reference to the XRController component
    public XRDirectInteractor directInteractor; // Reference to the XRDirectInteractor component
    public Collider directCollider;

    private IXRSelectInteractable _currentInteractable = null;
    private bool _selected = false;
    private bool _needInteractable = false;

    private void Update()
    {
        // Check input values from the Controllers
        bool gripPressed = xrController.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.5f;
        bool triggerPressed = xrController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.5f;

        if (_currentInteractable == null)
            _needInteractable = true;
        else
            _needInteractable = false;


        if (gripPressed || triggerPressed)
        {
            if (_currentInteractable != null && !_selected)
            {
                //Start grab interaction
                directInteractor.StartManualInteraction(_currentInteractable);
                _selected = true;
            }
        }
        else
        {
            if (_currentInteractable != null && _selected)
            {
                //End grab interaction
                directInteractor.EndManualInteraction();
                _selected = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Component" && _currentInteractable == null && _needInteractable)
        {
            other.TryGetComponent<XRGrabInteractable>(out var grabInteractable);
            _currentInteractable = grabInteractable;
            _needInteractable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Component" && _currentInteractable != null)
            _currentInteractable = null;
    }
}
