using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenarios;
using System.Linq;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    private List<DynamicEnvItem> _dynamicEnvItems;
    public Scenario scenario;
    private Scenario _previousScenario;

    private void Start()
    {
        _dynamicEnvItems = new List<DynamicEnvItem>(FindObjectsOfType<DynamicEnvItem>());
    }

    private void OnValidate()
    {
        if (scenario != _previousScenario)
        {
            ApplyScenarioLook();
            _previousScenario = scenario;
        }
    }

    private void ApplyScenarioLook()
    {
        Debug.Log("ApplyScenarioLook");
        foreach (var item in _dynamicEnvItems)
        {
            item.ChangeLook(scenario);
        }
    }
}
