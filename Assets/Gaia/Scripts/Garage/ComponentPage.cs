using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentPage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _currentCategoryTxt, _currentNameTxt, _currentFootprintTxt, _currentPriceTxt, _currentManufactureTxt, _currentUseTxt, _currentRecycleTxt, _totalFootprint, _totalEuro, _footprintBudget, _euroBudget;

    [SerializeField]
    Image _currentImg, _typeImg, _engineImg, _optionImg;

    void UpdateCurrentComponent(Component comp)
    {
        _currentCategoryTxt.text = comp.getCategory().ToString();
        _currentNameTxt.text = comp.getName().ToString();
        int footprint = comp.getManufactureFootprint() + comp.getUseFootprint() + comp.getRecycleFootprint();
        _currentFootprintTxt.text = footprint.ToString();
    }
}
