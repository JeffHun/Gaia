using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIStates;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _idlePage, _componentPage, _warningPage, _overviewPage;

    [SerializeField]
    int _footPrintBudget = 50000, _euroBudget = 10000;

    private UIState _currentState;

    void Start()
    {
        ChangeState(UIState.component);

        _idlePage.GetComponent<IdlePage>().setFootprintBudget(_footPrintBudget);
        _idlePage.GetComponent<IdlePage>().setEuroBudget(_euroBudget);
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
