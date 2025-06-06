using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float CO2WeighCoefBeef;
    public float CO2WeighCoefSaumon;
    public float CO2WeighCoefPorc;
    public float CO2WeighCoefChicken;
    public float CO2WeighCoefVeget;

    public float priceWeighCoefBeef;
    public float priceWeighCoefSaumon;
    public float priceWeighCoefPorc;
    public float priceWeighCoefChicken;
    public float priceWeighCoefVeget;
                                                                    // coef = goalWeight / volume
    private float volWeighCoefBeef = 492212.94510201359393711782741144f; // = 200/0.0004063282
    private float volWeighCoefSaumon = 568391.08944656896402766473110554f; // = 200/0.0003518704
    private float volWeighCoefPorc = 975728.74740822051469691425783632f; // = 200/0.000204975
    private float volWeighCoefChicken = 432234.49239894033391843475787737f;// = 200/0.0004627118
    private float volWeighCoefVeget = 1155332.088655563f;// = 200/0,0001731104

    public List<GameObject> meats = new List<GameObject>();

    public void SetMeats(List<GameObject> aMeats)
    {
        meats = aMeats;
    }

    public float GetVolWeighBeefCoef()
    {
        return volWeighCoefBeef;
    }

    public float GetVolWeighSaumonCoef()
    {
        return volWeighCoefSaumon;
    }

    public float GetVolWeighPorcCoef()
    {
        return volWeighCoefPorc;
    }

    public float GetVolWeighChickenCoef()
    {
        return volWeighCoefChicken;
    }

    public float GetVolWeighVegetCoef()
    {
        return volWeighCoefVeget;
    }

    public float GetPriceWeighBeefCoef()
    {
        return priceWeighCoefBeef;
    }

    public float GetPriceWeighSaumonCoef()
    {
        return priceWeighCoefSaumon;
    }

    public float GetPriceWeighPorcCoef()
    {
        return priceWeighCoefPorc;
    }

    public float GetPriceWeighChickenCoef()
    {
        return priceWeighCoefChicken;
    }

    public float GetPriceWeighVegetCoef()
    {
        return priceWeighCoefVeget;
    }

    public float GetCO2WeighBeefCoef()
    {
        return CO2WeighCoefBeef;
    }

    public float GetCO2WeighSaumonCoef()
    {
        return CO2WeighCoefSaumon;
    }

    public float GetCO2WeighPorcCoef()
    {
        return CO2WeighCoefPorc;
    }

    public float GetCO2WeighChickenCoef()
    {
        return CO2WeighCoefChicken;
    }

    public float GetCO2WeighVegetCoef()
    {
        return CO2WeighCoefVeget;
    }

    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
