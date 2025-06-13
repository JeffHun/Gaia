using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeightScale : MonoBehaviour
{
    private List<GameObject> meats = new List<GameObject>();
    public TextMeshProUGUI weighTxt;
    public TextMeshProUGUI footprintTxt;
    public TextMeshProUGUI priceTxt;
    private float totalWeigh;
    private float totalFootprint;
    private float totalPrice;
    private bool isPlatePresence = true;

    private void Start()
    {
        UpdateCanvas();
    }

    public void MeatEnter(GameObject aMeat)
    {
        meats.Add(aMeat);
        totalWeigh += aMeat.GetComponent<Meat>().GetWeigh();
        totalFootprint += aMeat.GetComponent<Meat>().GetFootprint();
        totalPrice += aMeat.GetComponent<Meat>().GetPrice();
        UpdateCanvas();
    }

    public void SetIsPlatePresence(bool aState)
    {
        isPlatePresence = aState;
    }

    public void MeatExit(GameObject aMeat)
    {
        for (int i = 0; i < meats.Count; i++)
        {
            if (meats[i] == aMeat)
            {
                meats.RemoveAt(i);
            }
        }
        totalWeigh -= aMeat.GetComponent<Meat>().GetWeigh();
        totalFootprint -= aMeat.GetComponent<Meat>().GetFootprint();
        totalPrice -= aMeat.GetComponent<Meat>().GetPrice();
        UpdateCanvas();
    }

    public void Reset()
    {
        meats.Clear();
        totalWeigh = 0;
        totalFootprint = 0;
        totalPrice = 0;
        UpdateCanvas();
    }

    public void UpdateCanvas()
    {
        if (isPlatePresence)
        {
            if (totalWeigh < 0)
                weighTxt.text = "Poids : 0g";
            else
                weighTxt.text = "Poids : " + Mathf.Round(totalWeigh).ToString() + "g";

            if (totalFootprint < 0)
                footprintTxt.text = "CO² : 0g";
            else
                footprintTxt.text = "CO² : " + Mathf.Round(totalFootprint).ToString() + "g";

            if (totalPrice < 0)
                priceTxt.text = "Prix : 0€";
            else
                priceTxt.text = "Prix : " + (Mathf.Floor(totalPrice * 100) / 100).ToString() + "€";
        }
        else
        {
            weighTxt.text = "Plateau mal positionné ou absent";
            footprintTxt.text = "";
            priceTxt.text = "";
        }
    }
}
