using Scenarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tree : DynamicEnvItem
{
    [SerializeField]
    private GameObject _TreeLeavesA, _TreeLeavesB, _TreeLeavesC, _TreeLeavesD;
    private GameObject _currentLeaves;

    public override void ChangeLook(Scenario scenario)
    {
        cleanTree();
        switch (scenario)
        {
            case Scenario.ScenarioA:
                _currentLeaves = Instantiate(_TreeLeavesA, transform.position, transform.rotation);
                break;
            case Scenario.ScenarioB:
                _currentLeaves = Instantiate(_TreeLeavesB, transform.position, transform.rotation);
                break;
            case Scenario.ScenarioC:
                _currentLeaves = Instantiate(_TreeLeavesC, transform.position, transform.rotation);
                break;
            case Scenario.ScenarioD:
                _currentLeaves = Instantiate(_TreeLeavesD, transform.position, transform.rotation);
                break;
        }
        _currentLeaves.transform.SetParent(transform);
    }

    void cleanTree()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
