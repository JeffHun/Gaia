using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : DynamicEnvItem
{
    public override void ChangeLook(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.scenarioA:
                gameObject.SetActive(true);
                break;
            case Scenario.scenarioB:
                gameObject.SetActive(true);
                break;
            case Scenario.scenarioC:
                gameObject.SetActive(false);
                break;
            case Scenario.scenarioD:
                gameObject.SetActive(false);
                break;
            case Scenario.scenarioE:
                gameObject.SetActive(false);
                break;
        }
    }
}
