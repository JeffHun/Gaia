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
    TextMeshProUGUI _currentCategoryTxt, _currentNameTxt, _currentFootprintTxt, _currentPriceTxt, _currentManufactureTxt, _currentUseTxt, _currentRecycleTxt;

    [SerializeField]
    Image _currentImg;

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
    }

    public void UIAddComponent(ComponentData comp)
    {
        bool isUpdate = false;
        switch(comp.Category)
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
                if(_components[i].ID == comp.ID)
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
                totalFootprint += _components[i].ManufactureFootprint;
                totalFootprint += _components[i].UseFootprint;
                totalFootprint += _components[i].RecycleFootprint;
                totalPrice += _components[i].Price;
            }
        }
    }

    public void UpdateCurrentComponent(ComponentData comp)
    {
        _currentImg.sprite = comp.ImageSprite;

        switch (comp.Category)
        {
            case Category.Moteur:
                SetBars(comp, (float)_componentsValues.GetMaxEngineFootprint());
                break;
            case Category.Type:
                SetBars(comp, (float)_componentsValues.GetMaxTypeFootprint());
                break;
            case Category.Options:
                SetBars(comp, (float)_componentsValues.GetMaxOptionFootprint());
                break;
        }

        SetTexts(comp);
    }

    private void SetBars(ComponentData comp, float footprint)
    {
        _manufactureSlider.SetBar(comp.ManufactureFootprint, footprint);
        _useSlider.SetBar(comp.UseFootprint, footprint);
        _recycleSlider.SetBar(comp.RecycleFootprint, footprint);
    }

    private void SetTexts(ComponentData comp)
    {
        _currentCategoryTxt.text = comp.Category.ToString() + "-";
        _currentNameTxt.text = comp.Name.ToString();

        int footprint = comp.ManufactureFootprint + comp.UseFootprint + comp.RecycleFootprint;
        _currentFootprintTxt.text = footprint.ToString();
        _currentPriceTxt.text = comp.Price.ToString();

        _currentManufactureTxt.text = comp.ManufactureFootprint.ToString();
        _currentUseTxt.text = comp.UseFootprint.ToString();
        _currentRecycleTxt.text = comp.RecycleFootprint.ToString();
    }
}
