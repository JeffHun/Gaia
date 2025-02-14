using Scenarios;
using System.Collections.Generic;
using UnityEngine;

public class ParkItems : DynamicEnvItem
{
    [SerializeField] private List<GameObject> _rocks = new List<GameObject>();
    [SerializeField] private List<GameObject> _nenuphars = new List<GameObject>();

    private static readonly bool[,] scenarioStates = new bool[,]
    {
        { true,  true,  true,  true  }, // Scenario A
        { false, true,  true,  true  }, // Scenario B
        { false, false, true,  true  }, // Scenario C
        { false, false, false, true  }, // Scenario D
        { false, false, false, false }  // Scenario E
    };

    public override void ChangeLook(Scenario scenario)
    {
        int scenarioIndex = (int)scenario;
        if (scenarioIndex < 0 || scenarioIndex >= scenarioStates.GetLength(0)) return;

        ApplyScenario(_rocks, scenarioIndex);
        ApplyScenario(_nenuphars, scenarioIndex);
    }

    private void ApplyScenario(List<GameObject> objects, int scenarioIndex)
    {
        for (int i = 0; i < objects.Count && i < scenarioStates.GetLength(1); i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(scenarioStates[scenarioIndex, i]);
        }
    }
}
