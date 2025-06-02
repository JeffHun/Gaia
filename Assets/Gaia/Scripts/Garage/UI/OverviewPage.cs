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
    TextMeshProUGUI _typeNameText, _engineNameText, _optionsNameText, 
        _typeFootprintText, _engineFootprintText, _optionsFootprintText, 
        _typePriceText, _enginePriceText, _optionsPriceText, _footprintText, 
        _footprintBudgetText, _priceText, _priceBudgetText;

    [SerializeField]
    Image _typeImage, _engineImage, _optionsImage;
    [SerializeField]
    Sprite _defaultImage;

    [SerializeField]
    Bar _footprintBar, _priceBar;

    [SerializeField]
    UIManager _UIManager;

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
            /*_typeNameText.text = components[0].Name;
            _engineNameText.text = components[1].Name;
            _optionsNameText.text = components[2].Name;

            _typeImage.sprite = components[0].ImageSprite;
            _engineImage.sprite = components[1].ImageSprite;
            _optionsImage.sprite = components[2].ImageSprite;*/
        
            int typeTotal= components[0].GetComponentTotalFootprint();
            int engineTotal= components[1].GetComponentTotalFootprint();
            int optionsTotal = components[2].GetComponentTotalFootprint();

            _typeFootprintText.text = typeTotal.ToString();
            _engineFootprintText.text = engineTotal.ToString();
            _optionsFootprintText.text = optionsTotal.ToString();

            _footprintBar.SetValues(typeTotal, engineTotal, optionsTotal);

            _footprintText.text = (typeTotal + engineTotal + optionsTotal).ToString();
            _footprintBudgetText.text = _UIManager.GetFootprintBudget().ToString();

            typeTotal = components[0].Price;
            engineTotal = components[1].Price;
            optionsTotal = components[2].Price;

            _typePriceText.text = typeTotal.ToString();
            _enginePriceText.text = engineTotal.ToString();
            _optionsPriceText.text = optionsTotal.ToString();

            _priceBar.SetValues(typeTotal, engineTotal, optionsTotal);

            _priceText.text = (typeTotal + engineTotal + optionsTotal).ToString();
            _priceBudgetText.text = _UIManager.GetPriceBudget().ToString();
        }
    }

    public void UIAddComponent(ComponentData component)
    {
        switch(component.Category)
        {
            case Category.Type:
                _typeNameText.text = component.Name;
                _typeImage.sprite = component.ImageSprite;
                break;
            case Category.Moteur:
                _engineNameText.text = component.Name;
                _engineImage.sprite = component.ImageSprite;
                break;
            case Category.Options:
                _optionsNameText.text = component.Name;
                _optionsImage.sprite = component.ImageSprite;
                break;
        }
    }
}
