using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    [SerializeField] float weigh;
    [SerializeField] float footPrint;
    [SerializeField] float price;

    [SerializeField] MeatType type;
    [SerializeField] Material crossSectionMat;

    private void Start()
    {
        CalculateWeight();
        CalculateFootPrint();
        CalculatePrice();
    }

    void CalculatePrice()
    {
        price = weigh * GetCoefPrice();
    }

    public float GetPrice()
    {
        return price;
    }

    void CalculateWeight()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        float volume = VolumeOfMesh(mesh);
        weigh = volume * GetCoefVolume();
    }
    public float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;

        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
    private float GetCoefVolume()
    {
        switch (type)
        {
            case MeatType.Beef:
                return GameManager.Instance.GetVolWeighBeefCoef();
            case MeatType.Saumon:
                return GameManager.Instance.GetVolWeighSaumonCoef();
            case MeatType.Porc:
                return GameManager.Instance.GetVolWeighPorcCoef();
            case MeatType.Chicken:
                return GameManager.Instance.GetVolWeighChickenCoef();
            case MeatType.Veget:
                return GameManager.Instance.GetVolWeighVegetCoef();
        }
        return 0;
    }

    private float GetCoefPrice()
    {
        switch (type)
        {
            case MeatType.Beef:
                return GameManager.Instance.GetPriceWeighBeefCoef();
            case MeatType.Saumon:
                return GameManager.Instance.GetPriceWeighSaumonCoef();
            case MeatType.Porc:
                return GameManager.Instance.GetPriceWeighPorcCoef();
            case MeatType.Chicken:
                return GameManager.Instance.GetPriceWeighChickenCoef();
            case MeatType.Veget:
                return GameManager.Instance.GetPriceWeighVegetCoef();
        }
        return 0;
    }

    private void CalculateFootPrint()
    {
        switch (type)
        {
            case MeatType.Beef:
                footPrint = weigh * GameManager.Instance.GetCO2WeighBeefCoef();
                break;
            case MeatType.Saumon:
                footPrint = weigh * GameManager.Instance.GetCO2WeighSaumonCoef();
                break;
            case MeatType.Porc:
                footPrint = weigh * GameManager.Instance.GetCO2WeighPorcCoef();
                break;
            case MeatType.Chicken:
                footPrint = weigh * GameManager.Instance.GetCO2WeighChickenCoef();
                break;
            case MeatType.Veget:
                footPrint = weigh * GameManager.Instance.GetCO2WeighVegetCoef();
                break;
        }
    }


    public void SetMeatType(MeatType aMeatType)
    {
        type = aMeatType;
    }

    public MeatType GetMeatType()
    {
        return type;
    }

    public Material GetCrossSectionMat()
    {
        return crossSectionMat;
    }

    public void SetCrossSection(Material aCrossSection)
    {
        crossSectionMat = aCrossSection;
    }


    public float GetWeigh()
    {
        return weigh;
    }

    public float GetFootprint()
    {
        return footPrint;
    }

    public enum MeatType
    {
        Beef,
        Saumon,
        Porc,
        Chicken,
        Veget
    }
}
