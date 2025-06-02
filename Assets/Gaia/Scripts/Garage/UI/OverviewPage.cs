using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using categories;
using TMPro;
using Components;

public class OverviewPage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _typeNameText, _engineNameText, _settingsNameText, 
        _typeFootprintText, _engineFootprintText, _settingsFootprintText, 
        _typePriceText, _enginePriceText, _settingsPriceText, _footprintText, 
        _footprintBudgetText, _priceText, _priceBudgetText;

    [SerializeField]
    Image _typeImage, _engineImage, _settingsImage;
    [SerializeField]
    Sprite _defaultImage;

    [SerializeField]
    Bar _footprintBar, _priceBar;

    [SerializeField]
    UIManager _UIManager;

    private int _typePrice = 0, _enginePrice = 0, _settingsPrice = 0;
    private int _typeFootprint = 0, _engineFootprint = 0, _settingsFootprint = 0;


    private bool AreComponentsValid(ComponentData[] components)
    {
        if (components.Length != 3)
        {
            Debug.LogWarning("There is no 3 components");
            return false;
        }

        foreach (ComponentData comp in components)
        {
            if (comp == null)
            {
                Debug.LogWarning("There is a null ComponentData");
                return false;
            }
        }
        return true;
    }

    public void UpdatePage(ComponentData[] components)
    {
        if (AreComponentsValid(components))
        {
            _typeFootprint = components[0].GetComponentTotalFootprint();
            _engineFootprint = components[1].GetComponentTotalFootprint();
            _settingsFootprint = components[2].GetComponentTotalFootprint();

            _typePrice = components[0].Price;
            _enginePrice = components[1].Price;
            _settingsPrice = components[2].Price;

            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        _typeFootprintText.text = _typeFootprint.ToString();
        _engineFootprintText.text = _engineFootprint.ToString();
        _settingsFootprintText.text = _settingsFootprint.ToString();

        _footprintBar.SetValues(_typeFootprint, _engineFootprint, _settingsFootprint);

        _footprintText.text = (_typeFootprint + _engineFootprint + _settingsFootprint).ToString();
        _footprintBudgetText.text = _UIManager.GetFootprintBudget().ToString();

        _typePriceText.text = _typePrice.ToString();
        _enginePriceText.text = _enginePrice.ToString();
        _settingsPriceText.text = _settingsPrice.ToString();

        _priceBar.SetValues(_typePrice, _enginePrice, _settingsPrice);

        _priceText.text = (_typePrice + _enginePrice + _settingsPrice).ToString();
        _priceBudgetText.text = _UIManager.GetPriceBudget().ToString();
    }

    public void UIAddComponent(ComponentData component)
    {
        switch(component.Category)
        {
            case Category.Type:
                _typeNameText.text = component.Name;
                _typeImage.sprite = component.ImageSprite;
                _typeFootprint = component.GetComponentTotalFootprint();
                _typePrice = component.Price;
                break;
            case Category.Moteur:
                _engineNameText.text = component.Name;
                _engineImage.sprite = component.ImageSprite;
                _engineFootprint = component.GetComponentTotalFootprint();
                _enginePrice = component.Price;
                break;
            case Category.Options:
                _settingsNameText.text = component.Name;
                _settingsImage.sprite = component.ImageSprite;
                _settingsFootprint = component.GetComponentTotalFootprint();
                _settingsPrice = component.Price;
                break;
        }
        UpdateUI();
    }
}
