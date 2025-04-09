using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdlePage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _footPrintTxt, _priceTxt;

    public void setFootprintBudget(int footPrintBudget)
    {
        _footPrintTxt.text = footPrintBudget.ToString();
    }

    public void setPriceBudget(int euroBudget)
    {
        _priceTxt.text = euroBudget.ToString();
    }
}
