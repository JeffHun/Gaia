using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : DynamicEnvItem
{
    [SerializeField]
    Light _sun;

    [SerializeField]
    List<Color> _sunColors = new List<Color>();

    public override void ChangeLook(Scenario scenario)
    {
        if((int)scenario <= _sunColors.Count)
            _sun.color = _sunColors[(int)scenario];
    }
}
