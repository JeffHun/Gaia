using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using categories;
using TMPro;

public class OverviewPage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _typeNameTxt, _engineNameTxt, _optionsNameTxt, _typeFootprintTxt, _engineFootprintTxt, _optionsFootprintTxt, _typePriceTxt, _enginePriceTxt, _optionsPriceTxt, _footprintTxt, _footprintBudgetTxt, _priceTxt, _priceBudgetTxt;

    [SerializeField]
    Image _typeImg, _engineImg, _optionsImg;
    [SerializeField]
    Sprite _defaultImg;

    [SerializeField]
    Bar _footprintBar, _priceBar;

    [SerializeField]
    UIManager _UIManager;

    private void Start()
    {
        // test
        Component typeComponent = new Component(69, Category.Type, "Berline", 5000, 2000, 500, 1000, _defaultImg);
        Component engineComponent = new Component(493, Category.Moteur, "Thermique", 3500, 2500, 150, 1550, _defaultImg);
        Component optionComponent = new Component(42, Category.Options, "Sécurité", 500, 50, 100, 1500, _defaultImg);
        Component[] components = new Component[3];
        components[0] = typeComponent;
        components[1] = engineComponent;
        components[2] = optionComponent;
        UpdatePage(components);
    }

    public void UpdatePage(Component[] components)
    {
        if(components.Length != 3)
        {
            Debug.LogWarning("There is no 3 components");
            return;
        }

        foreach(Component comp in components)
        {
            if(comp == null)
            {
                Debug.LogWarning("There is a null component");
                return;
            }
        }

        _typeNameTxt.text = components[0].GetName();
        _engineNameTxt.text = components[1].GetName();
        _optionsNameTxt.text = components[2].GetName();

        _typeImg.sprite = components[0].GetImg();
        _engineImg.sprite = components[1].GetImg();
        _optionsImg.sprite = components[2].GetImg();

        int typeTotal= components[0].GetManufactureFootprint() + components[0].GetUseFootprint() + components[0].GetRecycleFootprint();
        _typeFootprintTxt.text = typeTotal.ToString();
        int engineTotal= components[1].GetManufactureFootprint() + components[1].GetUseFootprint() + components[1].GetRecycleFootprint();
        _engineFootprintTxt.text = engineTotal.ToString();
        int optionsTotal= components[2].GetManufactureFootprint() + components[2].GetUseFootprint() + components[2].GetRecycleFootprint();
        _optionsFootprintTxt.text = optionsTotal.ToString();

        _footprintBar.SetValues(typeTotal, engineTotal, optionsTotal);

        _footprintTxt.text = (typeTotal + engineTotal + optionsTotal).ToString();
        _footprintBudgetTxt.text = _UIManager.GetPriceBudget().ToString();

        typeTotal = components[0].GetPrice();
        _typePriceTxt.text = typeTotal.ToString();
        engineTotal = components[1].GetPrice();
        _enginePriceTxt.text = engineTotal.ToString();
        optionsTotal = components[2].GetPrice();
        _optionsPriceTxt.text = optionsTotal.ToString();

        _priceBar.SetValues(typeTotal, engineTotal, optionsTotal);

        _priceTxt.text = (typeTotal + engineTotal + optionsTotal).ToString();
        _priceBudgetTxt.text = _UIManager.GetPriceBudget().ToString();
    }
}
