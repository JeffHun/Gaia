using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : DynamicEnvItem
{
    [SerializeField]
    private Material _skyA, _skyB, _skyC, _skyD, _skyE;
    private Material _sky;

    private bool _enableFog;
    [SerializeField]
    private Color _fogColorB, _fogColorC, _fogColorD, _fogColorE;
    private Color _fogColor;

    [SerializeField]
    private float _fogDensityB, _fogDensityC, _fogDensityD, _fogDensityE;
    private float _fogDensity;

    [SerializeField]
    private float _fogStartDistance = 0f, _fogEndDistance = 300f;

    public override void ChangeLook(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.scenarioA:
                _sky = _skyA;
                _enableFog = false;
                break;
            case Scenario.scenarioB:
                _sky = _skyB;
                _enableFog = true;
                _fogColor = _fogColorB;
                _fogDensity = _fogDensityB;
                break;
            case Scenario.scenarioC:
                _sky = _skyC;
                _enableFog = true;
                _fogColor = _fogColorC;
                _fogDensity = _fogDensityC;
                break;
            case Scenario.scenarioD:
                _sky = _skyD;
                _enableFog = true;
                _fogColor = _fogColorD;
                _fogDensity = _fogDensityD;
                break;
            case Scenario.scenarioE:
                _sky = _skyE;
                _enableFog = true;
                _fogColor = _fogColorE;
                _fogDensity = _fogDensityE;
                break;
        }
        RenderSettings.skybox = _sky;
        if(_enableFog)
        {
            RenderSettings.fog = _enableFog;
            RenderSettings.fogColor = _fogColor;
            RenderSettings.fogDensity = _fogDensity;
            RenderSettings.fogStartDistance = _fogStartDistance;
            RenderSettings.fogEndDistance = _fogEndDistance;
        }
    }
}
