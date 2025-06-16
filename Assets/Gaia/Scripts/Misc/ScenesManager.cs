using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    /*
     * Keep track of the score for final scene
     * Keep track of the scenes order of the user
     * 
     * Prepare Scenario Manager to handle scores
    */
    
    // SINGLETON
    public static ScenesManager Instance;

    // ATTRIBUTES
    private float _score = 0;
    private float _maxScore = 100;

    private string _kitchenSceneName = "Kitchen";
    private string _garageSceneName = "Garage";
    private string _townSceneName = "Town";

    private List<string> _scenes = new List<string>();

    private AsyncOperation _asyncOperation;

    // PROPERTIES
    public float Score { get { return _score; } private set { _score = value;  } }
    public float MaxScore { get { return _maxScore; } private set { _maxScore = value; } }

    // METHODS
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore(float score)
    {
        _score += score;
    }

    public void SwitchScene(string sceneName)
    {
        Debug.Log(sceneName);
        _scenes.Add(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchSceneAuto();
        }
    }

    public void SwitchSceneAuto()
    {
        if (_scenes.Count == 1)
        {
            switch(_scenes[0])
            {
                case "Kitchen":
                    SwitchScene(_garageSceneName); 
                    return;
                case "Garage":
                    SwitchScene(_kitchenSceneName);
                    return;
            }
        }
        if (_scenes.Count == 2)
        {
            SwitchScene(_townSceneName);
        }
    }
    
    public void SwitchAsyncSceneAuto()
    {
        if (_scenes.Count == 1)
        {
            switch (_scenes[0])
            {
                case "Kitchen":
                    StartCoroutine(AsyncSceneSwitch(_garageSceneName));
                    return;
                case "Garage":
                    StartCoroutine(AsyncSceneSwitch(_kitchenSceneName));
                    return;
            }
        }
        if (_scenes.Count == 2)
        {
            StartCoroutine(AsyncSceneSwitch(_townSceneName));
        }
    }

    public void LaunchScene()
    {
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