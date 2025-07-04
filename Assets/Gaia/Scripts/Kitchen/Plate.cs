using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Plate : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _weightTxt, _footprintTxt, _priceTxt;

    float _weight, _footprint, _price;

    List<string> _meats = new List<string>();

    [SerializeField]
    PlatesManager _plateManager;

    public List<string> GetMeats()
    {
        return _meats;
    }

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

    private void OnEnable()
    {
        ScenesManager.Instance.OnSceneChange.AddListener(SaveToLog);
    }

    private void OnTriggerEnter(Collider other)
    {
        Meat _meat;
        if(other.gameObject.TryGetComponent<Meat>(out _meat))
        {
            _weight += _meat.GetWeigh();
            _footprint += _meat.GetFootprint();
            _price += _meat.GetPrice();
            if(!_meats.Contains((other.gameObject.tag)))
                _meats.Add(other.gameObject.tag);
            UpdateUI();
            _plateManager.CheckPlate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Meat _meat;
        if (other.gameObject.TryGetComponent<Meat>(out _meat))
            if (!_meats.Contains((other.gameObject.tag)))
                _meats.Add(other.gameObject.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        Meat _meat;
        if (other.gameObject.TryGetComponent<Meat>(out _meat))
        {
            _weight -= _meat.GetWeigh();
            _footprint -= _meat.GetFootprint();
            _price -= _meat.GetPrice();
            _meats.Remove(other.gameObject.tag);
            UpdateUI();
            _plateManager.CheckPlate();
        }
    }

    void UpdateUI()
    {
        _weightTxt.text = Mathf.Round(_weight).ToString() + " g";
        _footprintTxt.text = Mathf.Round(_footprint).ToString() + " g";
        _priceTxt.text = (Mathf.Floor(_price * 100) / 100).ToString() + " €";
    }

    public void SaveToLog()
    {
        string text = "Plate :\n Meats: " + string.Join("\n\t", GetMeats().Select(s => $"{s}")) + 
            "\nWeight: " + GetWeight() + 
            "\nFootprint: " + GetFootprint() + 
            "\nPrice: " + GetPrice() + 
            "\n"; 

        FileLogsManager.Instance.LogToFile(text);
    }
}
