using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIStates;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _idlePage, _componentPage, _warningPage, _overviewPage;

    [SerializeField]
    int _footprintBudget = 50000, _euroBudget = 10000, _maxCompFootprint = 2500;

    private UIState _currentState;


    void Start()
    {
        ChangeState(UIState.component);

        _idlePage.GetComponent<IdlePage>().setFootprintBudget(_footprintBudget);
        _idlePage.GetComponent<IdlePage>().setEuroBudget(_euroBudget);
    }
    public int GetMaxFootprint()
    {
        return _maxCompFootprint;
    }

    public int GetFootprintBudget()
    {
        return _footprintBudget;
    }

    public int GetEuroBudget()
    {
        return _euroBudget;
    }

    public void ChangeState(UIState newState)
    {
        _idlePage.SetActive(false);
        _componentPage.SetActive(false);
        _warningPage.SetActive(false);
        _overviewPage.SetActive(false);

        switch (newState)
        {
            case UIState.idle:
                _idlePage.SetActive(true);
                break;
            case UIState.component:
                _componentPage.SetActive(true);
                break;
            case UIState.warning:
                _warningPage.SetActive(true);
                break;
            case UIState.overview:
                _overviewPage.SetActive(true);
                break;
        }

        _currentState = newState;
    }
}
