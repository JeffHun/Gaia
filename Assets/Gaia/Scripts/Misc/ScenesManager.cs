using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class ScenesManager : MonoBehaviour
{    
    // ATTRIBUTES
    private string _kitchenSceneName = "Kitchen";
    private string _garageSceneName = "Garage";
    private string _townSceneName = "Town";

    private string _userID = "";

    private bool _isVRMode = false;

    private AsyncOperation _asyncOperation;

    public UnityEvent OnSceneChange;


    // METHODS
    public void StartApp()
    {
        ToggleVRMode();
        FileLogsManager.Instance.CreateFile(_userID);
        SwitchScene("Garage");
    }

    public void UpdateId(string id)
    {
        _userID = id;
    }

    public void ToggleVRMode()
    {
        if (_isVRMode)
            DisableVR();
        else
            EnableVR();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void SwitchScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchSceneAuto();
        }
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name != "Start")
        {
            Reload();
        }
    }

    public void SwitchSceneAuto()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Kitchen":
                SwitchScene(_townSceneName);
                return;
            case "Garage":
                SwitchScene(_kitchenSceneName);
                return;
        }
    }
    
    public void SwitchAsyncSceneAuto()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Kitchen":
                StartCoroutine(AsyncSceneSwitch(_townSceneName));
                return;
            case "Garage":
                StartCoroutine(AsyncSceneSwitch(_kitchenSceneName));
                return;
        }
    }

    public void LaunchScene()
    {
        OnSceneChange.Invoke();
        _asyncOperation.allowSceneActivation = true;
    }

    IEnumerator AsyncSceneSwitch(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator StartXR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager;
        if(xrManager == null)
        {
            Debug.LogWarning("The XR Manager is not set up or ready");
            yield break;
        }

        xrManager.InitializeLoaderSync();
        if(xrManager.activeLoader == null)
        {
            Debug.LogWarning("The XR Loader failed to initialize");
            yield break;
        }

        xrManager.StartSubsystems();
    }

    private void StopXR()
    {
        var xrManager = XRGeneralSettings.Instance.Manager;
        if(xrManager == null)
        {
            Debug.LogWarning("The XR Manager is not set up or ready");
            return;
        }

        xrManager.StopSubsystems();
        xrManager.DeinitializeLoader();
    }
    
}