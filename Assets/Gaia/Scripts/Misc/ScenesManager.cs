using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    /*
     * Manage scene transitions
     * When launching at first, ask which scene to start with
     * Keep track of the score for final scene
     * Keep track of the scenes order of the user
     * 
     * Prepare Scenario Manager to handle scores
    */

    // SINGLETON
    public static ScenesManager Instance;

    // ATTRIBUTES
    private float _score = 0;

    private string _kitchenSceneName = "Kitchen";
    private string _garageSceneName = "Garage";
    private string _townSceneName = "Town";

    // PROPERTIES
    public float Score { get { return _score; } private set { _score = value;  } }

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

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}