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

    private int _maxCarFootrint = 0, _maxEngineFootprint = 0, _maxTypeFootprint = 0, _maxOptionFootprint = 0;

    void Start()
    {
        for(int i = 0; i < _dataComponents.Count; i++)
        {
            switch (_dataComponents[i].Category)
            {
                case Category.Moteur:
                    _maxEngineFootprint = Mathf.Max(
                        Mathf.Abs(_dataComponents[i].ManufactureFootprint), 
                        Mathf.Abs(_dataComponents[i].UseFootprint), 
                        Mathf.Abs(_dataComponents[i].RecycleFootprint), 
                        Mathf.Abs(_maxEngineFootprint)
                        );
                    break;
                case Category.Options:
                    _maxOptionFootprint = Mathf.Max(
                        Mathf.Abs(_dataComponents[i].ManufactureFootprint),
                        Mathf.Abs(_dataComponents[i].UseFootprint),
                        Mathf.Abs(_dataComponents[i].RecycleFootprint),
                        Mathf.Abs(_maxOptionFootprint)
                        );
                    break;
                case Category.Type:
                    _maxTypeFootprint = Mathf.Max(
                        Mathf.Abs(_dataComponents[i].ManufactureFootprint),
                        Mathf.Abs(_dataComponents[i].UseFootprint),
                        Mathf.Abs(_dataComponents[i].RecycleFootprint),
                        Mathf.Abs(_maxTypeFootprint)
                        );
                    break;
            }
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
