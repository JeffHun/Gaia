using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plate : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _weightTxt, _footprintTxt, _priceTxt;

    float _weight, _footprint, _price;

    public float GetWeight()
    {
        return _weight;
    }

    public float GetFootprint()
    {
        return _footprint;
    }

    public float GetPrice()
    {
        return _price;
    }

    private void OnTriggerEnter(Collider other)
    {
        Meat _meat;
        if(other.gameObject.TryGetComponent<Meat>(out _meat))
        {
            _weight += _meat.GetWeigh();
            _footprint += _meat.GetFootprint();
            _price += _meat.GetPrice();
        }
        UpdateUI();
    }

    private void OnTriggerExit(Collider other)
    {
        Meat _meat;
        if (other.gameObject.TryGetComponent<Meat>(out _meat))
        {
            _weight -= _meat.GetWeigh();
            _footprint -= _meat.GetFootprint();
            _price -= _meat.GetPrice();
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        _weightTxt.text = Mathf.Round(_weight).ToString() + " g";
        _footprintTxt.text = Mathf.Round(_footprint).ToString() + " g";
        _priceTxt.text = (Mathf.Floor(_price * 100) / 100).ToString() + " €";
    }
}
