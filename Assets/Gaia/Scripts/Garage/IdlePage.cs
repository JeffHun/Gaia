using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdlePage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _footPrintTxt, _euroTxt;

    public void setFootprintBudget(int footPrintBudget)
    {
        _footPrintTxt.text = footPrintBudget.ToString();
    }

    public void setEuroBudget(int euroBudget)
    {
        _euroTxt.text = euroBudget.ToString();
    }
}
