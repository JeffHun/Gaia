using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenarios;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private KeyCode _nextSceneKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _previousSceneKey = KeyCode.LeftArrow;

    private List<DynamicEnvItem> _dynamicEnvItems;
    public Scenario scenario = Scenario.scenarioA;
    private Scenario _previousScenario;

    private float _score = 0;

    //Boolean used to avoid calling ApplyScenarioLook if the enum's Unity UI does not match the default scenario value
    private bool _isDynamicEnvItemsFund = false; 

    private void Awake()
    {
        //scenario = Scenario.scenarioA;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_nextSceneKey))
        {
            if (scenario == Scenario.scenarioE)
            {
                Debug.LogWarning("Unable to load the next scenario because the current scenario is the last one.");
                return;
            }
            ++scenario;
            ApplyScenarioLook();
        }

        if (Input.GetKeyDown(_previousSceneKey))
        {
            if (scenario == Scenario.scenarioA)
            {
                Debug.LogWarning("Unable to load the previous scenario because the current scenario is the first one.");
                return;
            }
            --scenario;
            ApplyScenarioLook();
        }
    }

    private void Start()
    {
        // Allow time for the dynamically created object to appear
        StartCoroutine(Wait(.1f));
        _dynamicEnvItems = new List<DynamicEnvItem>(FindObjectsOfType<DynamicEnvItem>());
        _isDynamicEnvItemsFund = true;

        if (ScenesManager.Instance && SceneManager.GetActiveScene().name == "Town")
        {
            _score = ScenesManager.Instance.Score / ScenesManager.Instance.MaxScore;
            PickScenario(_score);
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ApplyScenarioLook();
    }

    private void OnValidate()
    {
        if (scenario != _previousScenario && _isDynamicEnvItemsFund)
        {
            ApplyScenarioLook();
            _previousScenario = scenario;
        }
    }

    private void ApplyScenarioLook()
    {
        foreach (var item in _dynamicEnvItems)
        {
            item.ChangeLook(scenario);
        }
    }

    private void PickScenario(float score)
    {
        if(score >= 0.8f)
        {
            scenario = Scenario.scenarioE;
        }
        else if(score >= 0.6f)
        {
            scenario = Scenario.scenarioD;
        }
        else if (score >= 0.4f)
        {
            scenario = Scenario.scenarioC;
        }
        else if (score >= 0.2f)
        {
            scenario = Scenario.scenarioB;
        }
        else
        {
            scenario = Scenario.scenarioA;
        }

        if(_isDynamicEnvItemsFund)
            ApplyScenarioLook();
    }
}
