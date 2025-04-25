using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIStates;
using categories;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _idlePage, _componentPage, _warningPage, _overviewPage, _componentsValues;

    [SerializeField]
    int _footprintBudget = 50000, _priceBudget = 10000;

    private UIState _currentState;


    void Start()
    {
        ChangeState(UIState.idle);

        _idlePage.GetComponent<IdlePage>().setFootprintBudget(_footprintBudget);
        _idlePage.GetComponent<IdlePage>().setPriceBudget(_priceBudget);
    }

    public int GetMaxComponentFootprint(Category category)
    {
        if(category == Category.Moteur)
            return _componentsValues.GetComponent<ComponentsValues>().GetMaxEngineFootprint();
        if (category == Category.Options)
            return _componentsValues.GetComponent<ComponentsValues>().GetMaxOptionFootprint();
        if (category == Category.Type)
            return _componentsValues.GetComponent<ComponentsValues>().GetMaxTypeFootprint();
        return 0;
    }

    public int GetMaxCarFootprint()
    {
        return _componentsValues.GetComponent<ComponentsValues>().GetMaxCarFootrint();
    }

    public int GetFootprintBudget()
    {
        return _footprintBudget;
    }

    public int GetPriceBudget()
    {
        return _priceBudget;
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
