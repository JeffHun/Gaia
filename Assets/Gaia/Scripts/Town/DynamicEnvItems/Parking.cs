using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking : DynamicEnvItem
{
    [SerializeField]
    List<GameObject> _cars = new List<GameObject>();

    [SerializeField]
    List<GameObject> _bikeLaneIcons = new List<GameObject>();

    public override void ChangeLook(Scenario scenario)
    {
        bool isBikeLane = true;
        int carsRatio = 0;
        switch (scenario)
        {
            case Scenario.scenarioA:
                isBikeLane = true;
                carsRatio = 0;
                break;
            case Scenario.scenarioB:
                isBikeLane = true;
                carsRatio = 0;
                break;
            case Scenario.scenarioC:
                isBikeLane = false;
                carsRatio = 30;
                break;
            case Scenario.scenarioD:
                isBikeLane = false;
                carsRatio = 60;
                break;
            case Scenario.scenarioE:
                isBikeLane = false;
                carsRatio = 100;
                break;
        }
        
        foreach (GameObject bikeLaneIcon in _bikeLaneIcons){bikeLaneIcon.SetActive(isBikeLane);}

        for(int i = 0; i < _cars.Count; ++i)
        {
            int rand = Random.Range(0,100);
            if(rand < carsRatio)
                _cars[i].SetActive(true);
            else
                _cars[i].SetActive(false);
        }
    }
}
