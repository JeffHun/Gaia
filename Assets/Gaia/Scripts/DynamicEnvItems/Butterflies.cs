using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterflies : DynamicEnvItem
{
    [SerializeField]
    GameObject _butterfly1, _butterfly2, _butterfly3;
    public override void ChangeLook(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.scenarioA:
                _butterfly1.SetActive(true);
                _butterfly2.SetActive(true);
                _butterfly3.SetActive(true);
                break;
            case Scenario.scenarioB:
                _butterfly1.SetActive(true);
                _butterfly2.SetActive(true);
                _butterfly3.SetActive(false);
                break;
            case Scenario.scenarioC:
                _butterfly1.SetActive(true);
                _butterfly2.SetActive(false);
                _butterfly3.SetActive(false);
                break;
            case Scenario.scenarioD:
                _butterfly1.SetActive(false);
                _butterfly2.SetActive(false);
                _butterfly3.SetActive(false);
                break;
            case Scenario.scenarioE:
                _butterfly1.SetActive(false);
                _butterfly2.SetActive(false);
                _butterfly3.SetActive(false);
                break;

        }
    }
}
