using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using categories;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

public class ComponentsValues : MonoBehaviour
{
    [SerializeField]
    List<ComponentDataSO> dataComponents = new List<ComponentDataSO>();

    [SerializeField]
    private int _maxCarFootrint = 0, _maxEngineFootprint = 0, _maxTypeFootprint = 0, _maxOptionFootprint = 0;

    void Start()
    {
        for(int i = 0; i < dataComponents.Count; i++)
        {
            if (dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = dataComponents[i].GetManufactureFootprint();
            if (dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = dataComponents[i].GetUseFootprint();
            if (dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = dataComponents[i].GetRecycleFootprint();

            if (dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = dataComponents[i].GetManufactureFootprint();
            if (dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = dataComponents[i].GetUseFootprint();
            if (dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = dataComponents[i].GetRecycleFootprint();

            if (dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = dataComponents[i].GetManufactureFootprint();
            if (dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = dataComponents[i].GetUseFootprint();
            if (dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = dataComponents[i].GetRecycleFootprint();
        }

        _maxCarFootrint = _maxEngineFootprint + _maxTypeFootprint + _maxOptionFootprint;
    }

    public int GetMaxEngineFootprint()
    {
        return _maxEngineFootprint;
    }

    public int GetMaxTypeFootprint()
    {
        return _maxTypeFootprint;
    }

    public int GetMaxOptionFootprint()
    {
        return _maxOptionFootprint;
    }

    public int GetMaxCarFootrint()
    {
        return _maxCarFootrint;
    }
}
