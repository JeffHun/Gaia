using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenarios;
using System.Linq;

public class ScenarioManager : MonoBehaviour
{
    private List<DynamicEnvItem> _dynamicEnvItems;
    public Scenario scenario = Scenario.scenarioA;
    private Scenario _previousScenario;

    //Boolean used to avoid calling ApplyScenarioLook if the enum's Unity UI does not match the default scenario value
    private bool _isDynamicEnvItemsFund = false; 

    private void Awake()
    {
        scenario = Scenario.scenarioA;
    }

    private void Start()
    {
        _dynamicEnvItems = new List<DynamicEnvItem>(FindObjectsOfType<DynamicEnvItem>());
        _isDynamicEnvItemsFund = true;
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
