using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class XRManager : MonoBehaviour
{
    private bool _isVRMode = false;

    public void Init()
    {
        ToggleVRMode();
    }

    public void ToggleVRMode()
    {
        if (_isVRMode)
            DisableVR();
        else
            EnableVR();
    }
    private void EnableVR()
    {
        StartCoroutine(StartXR());
        _isVRMode = true;
    }

    private void DisableVR()
    {
        StopXR();
        _isVRMode = false;
    }
    private IEnumerator StartXR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager;
        if (xrManager == null)
        {
            Debug.LogWarning("The XR Manager is not set up or ready");
            yield break;
        }

        xrManager.InitializeLoaderSync();
        if (xrManager.activeLoader == null)
        {
            Debug.LogWarning("The XR Loader failed to initialize");
            yield break;
        }

        xrManager.StartSubsystems();
    }

    private void StopXR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager;
        if (xrManager == null)
        {
            Debug.LogWarning("The XR Manager is not set up or ready");
            return;
        }

        xrManager.StopSubsystems();
        xrManager.DeinitializeLoader();
    }
}