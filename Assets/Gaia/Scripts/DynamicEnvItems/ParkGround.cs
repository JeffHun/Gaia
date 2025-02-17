using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkGround : DynamicEnvItem
{
    [SerializeField]
    GameObject _parkGround01, _parkGround02, _parkGround03, _parkGround04, _parkGround05;
    
    public override void ChangeLook(Scenario scenario)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        switch (scenario)
        {
            case Scenario.scenarioA:
                Instantiate(_parkGround01, transform);
                break;
            case Scenario.scenarioB:
                Instantiate(_parkGround02, transform.position, Quaternion.identity).transform.SetParent(transform);
                break;
            case Scenario.scenarioC:
                Instantiate(_parkGround03, transform.position, Quaternion.identity).transform.SetParent(transform);
                break;
            case Scenario.scenarioD:
                Instantiate(_parkGround04, transform.position, Quaternion.identity).transform.SetParent(transform);
                break;
            case Scenario.scenarioE:
                Instantiate(_parkGround05, transform.position, Quaternion.identity).transform.SetParent(transform);
                break;
        }
    }
}
