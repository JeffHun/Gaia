using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using categories;

public class ComponentPage : MonoBehaviour
{
    [SerializeField]
    UIManager _UImanager;

    [SerializeField]
    TextMeshProUGUI _currentCategoryTxt, _currentNameTxt, _currentFootprintTxt, _currentPriceTxt, _currentManufactureTxt, _currentUseTxt, _currentRecycleTxt, _typeTxt, _engineTxt, _optionTxt, _totalFootprint, _totalPrice, _footprintBudget, _priceBudget;

    [SerializeField]
    Image _currentImg, _manufactureSlider, _useSlider, _recycleSlider, _typeImg, _engineImg, _optionImg;

    Component[] components;

    [SerializeField]
    Sprite _unknowImg, _defaultImg;

    private void Start()
    {
        components = new Component[3];
        _priceBudget.text = _UImanager.GetPriceBudget().ToString();
        _footprintBudget.text = _UImanager.GetFootprintBudget().ToString();

        // test
        Component typeComponent = new Component(69, Category.Type, "Berline", 5000, 2000, 500, 1000, _defaultImg);
        Component noAddedEngineComponent = new Component(493, Category.Moteur, "Thermique", 3500, 2500, 150, 1550, _defaultImg);
        Component optionComponent = new Component(42, Category.Options, "Sécurité", 500, 50, 100, 1500, _defaultImg);
        AddComponent(typeComponent);
        AddComponent(optionComponent);

        RemoveComponent(optionComponent);
        RemoveComponent(noAddedEngineComponent);

        Component engineComponent = new Component(666, Category.Moteur, "Electrique", 6000, 1000, 150, 2250, _defaultImg);
        UpdateCurrentComponent(engineComponent);
    }

    void AddComponent(Component comp)
    {
        bool isUpdate = false;
        switch(comp.GetCategory())
        {
            case Category.Type:
                components[0] = comp;
                isUpdate = true;
                break;
            case Category.Moteur:
                components[1] = comp;
                isUpdate = true;
                break;
            case Category.Options:
                components[2] = comp;
                isUpdate = true;
                break;
        }

        if(isUpdate)
            UpdateComponentsPart();
    }

    void RemoveComponent(Component comp)
    {
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i] != null)
            {
                if(components[i].GetId() == comp.GetId())
                {
                    components[i] = null;
                    UpdateComponentsPart();
                    return;
                }
            }
        }
    }

    void UpdateComponentsPart()
    {
        int totalFootprint = 0;
        int totalPrice = 0;

        for(int i = 0; i < components.Length; i++)
        {
            if(components[i] != null)
            {
                totalFootprint += components[i].GetManufactureFootprint();
                totalFootprint += components[i].GetUseFootprint();
                totalFootprint += components[i].GetRecycleFootprint();
                totalPrice += components[i].GetPrice();
            }
        }

        _totalFootprint.text = totalFootprint.ToString();
        _totalPrice.text = totalPrice.ToString();

        if (components[0] != null)
        {
            _typeTxt.text = "Type -" + components[0].GetName();
            _typeImg.sprite = components[0].GetImg();
        }
        else
        {
            _typeTxt.text = "Type";
            _typeImg.sprite = _unknowImg;
        }

        if (components[1] != null)
        {
            _engineTxt.text = "Moteur -" + components[1].GetName();
            _engineImg.sprite = components[1].GetImg();
        }
        else
        {
            _engineTxt.text = "Moteur";
            _engineImg.sprite = _unknowImg;
        }

        if (components[2] != null)
        {
            _optionTxt.text = "Options -" + components[2].GetName();
            _optionImg.sprite = components[2].GetImg();
        }
        else
        {
            _optionTxt.text = "Options";
            _optionImg.sprite = _unknowImg;
        }

    }

    void UpdateCurrentComponent(Component comp)
    {
        _currentCategoryTxt.text = comp.GetCategory().ToString() + " -";
        _currentNameTxt.text = comp.GetName().ToString();

        int footprint = comp.GetManufactureFootprint() + comp.GetUseFootprint() + comp.GetRecycleFootprint();
        _currentFootprintTxt.text = footprint.ToString();
        _currentPriceTxt.text = comp.GetPrice().ToString();

        _currentImg.sprite = comp.GetImg();

        _manufactureSlider.fillAmount = comp.GetManufactureFootprint() / (float)_UImanager.GetMaxFootprint();
        _currentManufactureTxt.text = comp.GetManufactureFootprint().ToString();

        _useSlider.fillAmount = comp.GetUseFootprint() / (float)_UImanager.GetMaxFootprint();
        _currentUseTxt.text = comp.GetUseFootprint().ToString();

        _recycleSlider.fillAmount = comp.GetRecycleFootprint() / (float)_UImanager.GetMaxFootprint();
        _currentRecycleTxt.text = comp.GetRecycleFootprint().ToString();
    }
}
