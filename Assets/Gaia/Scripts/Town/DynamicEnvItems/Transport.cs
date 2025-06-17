using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : DynamicEnvItem
{
    [SerializeField]
    private GameObject _tram, _bus;

    public override void ChangeLook(Scenario scenario)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        GameObject transport = null;

        switch(scenario)
        {
            case Scenario.scenarioA:
                transport = Instantiate(_tram, transform.position, transform.rotation);
                break;
            case Scenario.scenarioB:
                transport = Instantiate(_tram, transform.position, transform.rotation);
                break;
            case Scenario.scenarioC:
                transport = Instantiate(_bus, transform.position, _bus.transform.rotation);
                break;
            case Scenario.scenarioD:
                transport = Instantiate(_bus, transform.position, _bus.transform.rotation);
                break;
            case Scenario.scenarioE:
                transport = Instantiate(_bus, transform.position, _bus.transform.rotation);
                break;
        }

        if(transport != null)
        {
            transport.transform.SetParent(transform);
        }
    }
}
