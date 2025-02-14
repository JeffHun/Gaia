using Scenarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Tree : DynamicEnvItem
{
    [SerializeField]
    private GameObject _treeLeavesA, _treeLeavesB, _treeLeavesC, _treeLeavesD;


    public override void ChangeLook(Scenario scenario)
    {
        if(transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        GameObject currentLeaves = null;

        switch (scenario)
        {
            case Scenario.scenarioA:
                currentLeaves = Instantiate(_treeLeavesA, transform.position, transform.rotation);
                break;
            case Scenario.scenarioB:
                currentLeaves = Instantiate(_treeLeavesB, transform.position, transform.rotation);
                break;
            case Scenario.scenarioC:
                currentLeaves = Instantiate(_treeLeavesC, transform.position, transform.rotation);
                break;
            case Scenario.scenarioD:
                currentLeaves = Instantiate(_treeLeavesD, transform.position, transform.rotation);
                break;
        }
        if(currentLeaves != null)
            currentLeaves.transform.SetParent(transform);
    }
}
