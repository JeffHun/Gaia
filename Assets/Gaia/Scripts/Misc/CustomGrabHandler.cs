using Components;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabHandler : MonoBehaviour
{
    public XRController xrController; // Reference to the XRController component
    public XRDirectInteractor directInteractor; // Reference to the XRDirectInteractor component
    public List<string> TagsList = new List<string> { "Component", "Beef", "Chicken", "Porc", "Saumon", "Knife", "Veget" };


    private IXRSelectInteractable _currentInteractable = null;
    private bool _selected = false;
    private bool _needInteractable = false;

    private void Update()
    {
        if (!directInteractor)
            return;
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
            if (directInteractor.interactablesSelected.Count >= 1 &&
                directInteractor.interactablesSelected[0] != null &&
                directInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>())
                directInteractor.interactablesSelected[0].transform.GetComponent<ComponentData>().SetCompStatus(ComponentStatus.Hand);
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
        if(other.transform.parent && TagsList.Contains(other.transform.parent.tag) && _currentInteractable == null && _needInteractable)
        {
            other.transform.parent.transform.TryGetComponent<XRGrabInteractable>(out var grabInteractable);
            _currentInteractable = grabInteractable;
            _needInteractable = false;
        }
        else if(TagsList.Contains(other.tag) && _currentInteractable == null && _needInteractable)
        {
            XRGrabInteractable grabInteractable = null;
            if (other.transform.parent && other.transform.parent.GetComponent<XRGrabInteractable>())
                other.transform.parent.transform.TryGetComponent(out grabInteractable);
            else
                other.transform.TryGetComponent(out grabInteractable);
            _currentInteractable = grabInteractable;
            _needInteractable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Handle" && _currentInteractable != null)
        {
            Debug.Log("Handle is out of range");
            _currentInteractable = null;
            directInteractor.EndManualInteraction();
            _selected = false;
        }

        if (other.transform.parent && TagsList.Contains(other.transform.parent.tag) && _currentInteractable != null)
            _currentInteractable = null;
        else if (TagsList.Contains(other.tag) && _currentInteractable != null)
            _currentInteractable = null;

    }

    public void SelectObject(IXRSelectInteractable selectInteractable)
    {
        directInteractor.StartManualInteraction(selectInteractable);
        _selected = true;
    }

    public void EndSelectObject(IXRSelectInteractable selectInteractable)
    {
        directInteractor.EndManualInteraction();
        _selected = false;
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;
    }
}
