using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : DynamicEnvItem
{
    [SerializeField]
    List<GameObject> _pigeons = new List<GameObject>();

    [SerializeField]
    GameObject _magpie;

    [SerializeField]
    GameObject _cranes;

    private static readonly bool[,] _pigeonStates = new bool[,]
    {
        { true, true,  true, true}, // Scenario A
        { true, true,  true, false}, // Scenario B
        { true, true, false, false}, // Scenario C
        { true, false, false, false}, // Scenario D
        { false, false, false, false}  // Scenario E
    };

    public override void ChangeLook(Scenario scenario)
    {
        int scenarioIndex = (int)scenario;
        if (scenarioIndex < 0 || scenarioIndex >= _pigeonStates.GetLength(0)) return;

        ApplyScenario(_pigeons, scenarioIndex);

        switch (scenario)
        {
            case Scenario.scenarioA:
                _magpie.SetActive(true);
                _cranes.SetActive(true);
                break;
            case Scenario.scenarioB:
                _magpie.SetActive(true);
                _cranes.SetActive(true);
                break;
            case Scenario.scenarioC:
                _magpie.SetActive(false);
                _cranes.SetActive(false);
                break;
            case Scenario.scenarioD:
                _magpie.SetActive(false);
                _cranes.SetActive(false);
                break;
            case Scenario.scenarioE:
                _magpie.SetActive(false);
                _cranes.SetActive(false);
                break;
        }
    }

    private void ApplyScenario(List<GameObject> objects, int scenarioIndex)
    {
        for (int i = 0; i < objects.Count && i < _pigeonStates.GetLength(1); i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(_pigeonStates[scenarioIndex, i]);
        }
    }
}
