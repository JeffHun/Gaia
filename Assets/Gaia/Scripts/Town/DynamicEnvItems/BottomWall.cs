using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWall : DynamicEnvItem
{
    [SerializeField]
    private GameObject _woodenBar;
    private bool isWoodenBar = false;

    public override void ChangeLook(Scenario scenario)
    {
        if(scenario <= Scenario.scenarioB)
        {
            if(!isWoodenBar)
            {
                GameObject woodenBar = Instantiate(_woodenBar, transform.position, transform.rotation);
                woodenBar.transform.SetParent(transform);
                isWoodenBar = true;
            }
        }
        else
        {
            if(isWoodenBar)
            {
                Destroy(transform.GetChild(0).gameObject);
                isWoodenBar = false;
            }
        }
    }
}
