using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : DynamicEnvItem
{
    [SerializeField]
    GameObject _water;

    [SerializeField]
    Color32 _waterDeepColorA, _waterShallowColorA, _waterDeepColorB, _waterShallowColorB, _waterDeepColorC, _waterShallowColorC, _waterDeepColorD, _waterShallowColorD;

    Material _material;

    private void Start()
    {
        _material = _water.GetComponent<MeshRenderer>().material;
    }

    public override void ChangeLook(Scenario scenario)
    {
        _water.SetActive(true);
        switch (scenario)
        {
            case Scenario.scenarioA:
                _water.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                _material.SetColor("_DeepColor", _waterDeepColorA);
                _material.SetColor("_ShallowColor", _waterDeepColorA);
                _material.SetFloat("_Smoothness", 1f);
                break;
            case Scenario.scenarioB:
                _water.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                _material.SetColor("_DeepColor", _waterDeepColorB);
                _material.SetColor("_ShallowColor", _waterDeepColorB);
                _material.SetFloat("_Smoothness", 1f);
                break;
            case Scenario.scenarioC:
                _water.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
                _material.SetColor("_DeepColor", _waterDeepColorC);
                _material.SetColor("_ShallowColor", _waterDeepColorC);
                _material.SetFloat("_Smoothness", .5f);
                break;
            case Scenario.scenarioD:
                _water.transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                _material.SetColor("_DeepColor", _waterDeepColorD);
                _material.SetColor("_ShallowColor", _waterDeepColorD);
                _material.SetFloat("_Smoothness", .25f);
                break;
            case Scenario.scenarioE:
                _water.SetActive(false);
                break;
        }
    }
}
