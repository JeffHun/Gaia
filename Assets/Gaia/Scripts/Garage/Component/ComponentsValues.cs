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
    List<ComponentDataSO> _dataComponents = new List<ComponentDataSO>();

    [SerializeField]
    private int _maxCarFootrint = 0, _maxEngineFootprint = 0, _maxTypeFootprint = 0, _maxOptionFootprint = 0;

    void Start()
    {
        for(int i = 0; i < _dataComponents.Count; i++)
        {
            if (_dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(_dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = _dataComponents[i].GetManufactureFootprint();
            if (_dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(_dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = _dataComponents[i].GetUseFootprint();
            if (_dataComponents[i].GetCategory() == Category.Moteur && Mathf.Abs(_dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxEngineFootprint))
                _maxEngineFootprint = _dataComponents[i].GetRecycleFootprint();

            if (_dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(_dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = _dataComponents[i].GetManufactureFootprint();
            if (_dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(_dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = _dataComponents[i].GetUseFootprint();
            if (_dataComponents[i].GetCategory() == Category.Type && Mathf.Abs(_dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxTypeFootprint))
                _maxTypeFootprint = _dataComponents[i].GetRecycleFootprint();

            if (_dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(_dataComponents[i].GetManufactureFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = _dataComponents[i].GetManufactureFootprint();
            if (_dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(_dataComponents[i].GetUseFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = _dataComponents[i].GetUseFootprint();
            if (_dataComponents[i].GetCategory() == Category.Options && Mathf.Abs(_dataComponents[i].GetRecycleFootprint()) > Mathf.Abs(_maxOptionFootprint))
                _maxOptionFootprint = _dataComponents[i].GetRecycleFootprint();
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
