using UnityEngine;
using UnityEngine.UI;
using TMPro;
using categories;
using Components;

public class ComponentPage : MonoBehaviour
{
    [SerializeField]
    UIManager _UImanager;

    [SerializeField]
    ComponentsValues _componentsValues;

    [SerializeField]
    TextMeshProUGUI _currentCategoryTxt, _currentNameTxt, _currentFootprintTxt, _currentPriceTxt, _currentManufactureTxt, _currentUseTxt, _currentRecycleTxt, _typeTxt, _engineTxt, _optionTxt, _totalFootprint, _totalPrice, _footprintBudget, _priceBudget;

    [SerializeField]
    Image _currentImg, _typeImg, _engineImg, _optionImg;

    [SerializeField]
    SliderGradient _manufactureSlider, _useSlider, _recycleSlider;

    ComponentData[] _components;

    [SerializeField]
    Sprite _unknowImg;

    private void Awake()
    {
        _components = new ComponentData[3];
    }

    private void Start()
    {
        _priceBudget.text = _UImanager.GetPriceBudget().ToString();
        _footprintBudget.text = _UImanager.GetFootprintBudget().ToString();
    }

    public void UIAddComponent(ComponentData comp)
    {
        bool isUpdate = false;
        switch(comp.GetCategory())
        {
            case Category.Type:
                _components[0] = comp;
                isUpdate = true;
                break;
            case Category.Moteur:
                _components[1] = comp;
                isUpdate = true;
                break;
            case Category.Options:
                _components[2] = comp;
                isUpdate = true;
                break;
        }

        if (isUpdate)
        {
            UpdateComponentsPart();
        }
    }

    public void UIRemoveComponent(ComponentData comp)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] != null)
            {
                if(_components[i].GetId() == comp.GetId())
                {
                    _components[i] = null;
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

        for(int i = 0; i < _components.Length; i++)
        {
            if(_components[i] != null)
            {
                totalFootprint += _components[i].GetManufactureFootprint();
                totalFootprint += _components[i].GetUseFootprint();
                totalFootprint += _components[i].GetRecycleFootprint();
                totalPrice += _components[i].GetPrice();
            }
        }

        _totalFootprint.text = totalFootprint.ToString();
        _totalPrice.text = totalPrice.ToString();

        if (_components[0] != null)
        {
            _typeTxt.text = "Type -" + _components[0].GetName();
            _typeImg.sprite = _components[0].GetImg();
        }
        else
        {
            _typeTxt.text = "Type";
            _typeImg.sprite = _unknowImg;
        }

        if (_components[1] != null)
        {
            _engineTxt.text = "Moteur -" + _components[1].GetName();
            _engineImg.sprite = _components[1].GetImg();
        }
        else
        {
            _engineTxt.text = "Moteur";
            _engineImg.sprite = _unknowImg;
        }

        if (_components[2] != null)
        {
            _optionTxt.text = "Options -" + _components[2].GetName();
            _optionImg.sprite = _components[2].GetImg();
        }
        else
        {
            _optionTxt.text = "Options";
            _optionImg.sprite = _unknowImg;
        }

        int remainingPriceBudget = _UImanager.GetPriceBudget() - totalPrice;
        int remainingFootprintBudget = _UImanager.GetFootprintBudget() - totalFootprint;

        if (remainingPriceBudget > 0)
            _priceBudget.text = remainingPriceBudget.ToString();
        else
            _priceBudget.text = "0";

        if (remainingFootprintBudget > 0)
            _footprintBudget.text = remainingFootprintBudget.ToString();
        else
            _footprintBudget.text = "0";
    }

    public void UpdateCurrentComponent(ComponentData comp)
    {
        _currentCategoryTxt.text = comp.GetCategory().ToString() + "-";
        _currentNameTxt.text = comp.GetName().ToString();

        int footprint = comp.GetManufactureFootprint() + comp.GetUseFootprint() + comp.GetRecycleFootprint();
        _currentFootprintTxt.text = footprint.ToString();
        _currentPriceTxt.text = comp.GetPrice().ToString();

        _currentImg.sprite = comp.GetImg();

        if(comp.GetCategory() == Category.Moteur)
        {
            _manufactureSlider.SetBar(comp.GetManufactureFootprint(), (float)_componentsValues.GetMaxEngineFootprint());
            _useSlider.SetBar(comp.GetUseFootprint(), (float)_componentsValues.GetMaxEngineFootprint());
            _recycleSlider.SetBar(comp.GetRecycleFootprint(), (float)_componentsValues.GetMaxEngineFootprint());
        }

        if (comp.GetCategory() == Category.Options)
        {
            _manufactureSlider.SetBar(comp.GetManufactureFootprint(), (float)_componentsValues.GetMaxOptionFootprint());
            _useSlider.SetBar(comp.GetUseFootprint(), (float)_componentsValues.GetMaxOptionFootprint());
            _recycleSlider.SetBar(comp.GetRecycleFootprint(), (float)_componentsValues.GetMaxOptionFootprint());
        }

        if (comp.GetCategory() == Category.Type)
        {
            _manufactureSlider.SetBar(comp.GetManufactureFootprint(), (float)_componentsValues.GetMaxTypeFootprint());
            _useSlider.SetBar(comp.GetUseFootprint(), (float)_componentsValues.GetMaxTypeFootprint());
            _recycleSlider.SetBar(comp.GetRecycleFootprint(), (float)_componentsValues.GetMaxTypeFootprint());
        }

        _currentManufactureTxt.text = comp.GetManufactureFootprint().ToString();
        _currentUseTxt.text = comp.GetUseFootprint().ToString();
        _currentRecycleTxt.text = comp.GetRecycleFootprint().ToString();
    }
}
