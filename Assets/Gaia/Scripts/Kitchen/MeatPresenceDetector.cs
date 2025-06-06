using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPresenceDetector : MonoBehaviour
{
    public WeightScale scale;
    public MeatsManager meatManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Meat>())
        {
            other.gameObject.transform.parent = transform;
            scale.MeatEnter(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Meat>())
        {
            switch(other.GetComponent<Meat>().GetMeatType())
            {
                case Meat.MeatType.Beef:
                    meatManager.ParentedBeef(other.gameObject);
                    break;
                case Meat.MeatType.Saumon:
                    meatManager.ParentedSaumon(other.gameObject);
                    break;
                case Meat.MeatType.Porc:
                    meatManager.ParentedPorc(other.gameObject);
                    break;
                case Meat.MeatType.Chicken:
                    meatManager.ParentedChicken(other.gameObject);
                    break;
                case Meat.MeatType.Veget:
                    meatManager.ParentedChicken(other.gameObject);
                    break;
            }
            scale.MeatExit(other.gameObject);
        }
    }
}
