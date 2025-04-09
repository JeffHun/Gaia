using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayManager : MonoBehaviour
{
    [SerializeField] private XRController _leftControllerRay;
    [SerializeField] private XRController _rightControllerRay;
    ActionBasedController _actionBasedController;
    [SerializeField] private InputHelpers.Button _activationInput;
    [SerializeField] private float _activationThreshold;

    // Update is called once per frame
    void Update()
    {
        if (_leftControllerRay)
        {
            _leftControllerRay.gameObject.SetActive(CheckIfActivated(_leftControllerRay) && (!_rightControllerRay || !_rightControllerRay.gameObject.activeSelf));
        }

        if (_rightControllerRay)
        {
            _rightControllerRay.gameObject.SetActive(CheckIfActivated(_rightControllerRay) && (!_leftControllerRay || !_leftControllerRay.gameObject.activeSelf));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _activationInput, out bool isActivated, _activationThreshold);
        return isActivated;
    }

}