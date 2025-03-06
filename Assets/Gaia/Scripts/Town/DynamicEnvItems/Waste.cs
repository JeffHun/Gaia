using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : DynamicEnvItem
{
    [SerializeField]
    List<GameObject> _waste = new List<GameObject>();

    private static readonly bool[,] _wasteStates = new bool[,]
    {
        { false, false, false, false, false, false, false, false, false, false}, // Scenario A
        { false, false, false, false, false, false, false, false, false, false}, // Scenario B
        { true, false, false, false, false, false, false, false, false, false}, // Scenario C
        { true, true, true, true, false, false, false, false, false, false}, // Scenario D
        { true, true, true, true, true, true, true, true, true, true}, // Scenario E
    };

    public override void ChangeLook(Scenario scenario)
    {
        int scenarioIndex = (int)scenario;
        if (scenarioIndex < 0 || scenarioIndex >= _wasteStates.GetLength(0)) return;

        ApplyScenario(_waste, scenarioIndex);
    }

    private void ApplyScenario(List<GameObject> objects, int scenarioIndex)
    {
        for (int i = 0; i < objects.Count && i < _wasteStates.GetLength(1); i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(_wasteStates[scenarioIndex, i]);
        }
    }
}
