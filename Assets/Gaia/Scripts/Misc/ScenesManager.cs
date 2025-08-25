using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{    
    // ATTRIBUTES
    private string _kitchenSceneName = "Kitchen";
    private string _garageSceneName = "Garage";
    private string _townSceneName = "Town";

    private string _userID = "";

    private AsyncOperation _asyncOperation;
    [SerializeField]
    private XRManager _xrManager;

    public UnityEvent OnSceneChange;


    // METHODS
    public void StartApp()
    {
        FileLogsManager.Instance.CreateFile(_userID);
        SwitchScene("Garage");
    }

    public void UpdateId(string id)
    {
        _userID = id;
    }


    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void SwitchScene(string sceneName)
    {
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
}