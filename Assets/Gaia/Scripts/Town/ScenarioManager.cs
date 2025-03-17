using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenarios;
using System.Linq;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private KeyCode _nextSceneKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _previousSceneKey = KeyCode.LeftArrow;

    private List<DynamicEnvItem> _dynamicEnvItems;
    public Scenario scenario;
    private Scenario _previousScenario;

    //Boolean used to avoid calling ApplyScenarioLook if the enum's Unity UI does not match the default scenario value
    private bool _isDynamicEnvItemsFund = false; 

    private void Awake()
    {
        scenario = Scenario.scenarioA;
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
}
