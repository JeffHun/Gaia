using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPanel: DynamicEnvItem
{
    [SerializeField]
    Texture2D _texA, _texB, _texC, _texD, _texE;

    Material _mat;
    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
    }

    public override void ChangeLook(Scenario scenario)
    {
        _mat = GetComponent<MeshRenderer>().material;
        switch (scenario)
        {
            case Scenario.scenarioA:
                _mat.mainTexture = _texA;
                break;
            case Scenario.scenarioB:
                _mat.mainTexture = _texB;
                break;
            case Scenario.scenarioC:
                _mat.mainTexture = _texC;
                break;
            case Scenario.scenarioD:
                _mat.mainTexture = _texD;
                break;
            case Scenario.scenarioE:
                _mat.mainTexture = _texE;
                break;
        }
    }
}
