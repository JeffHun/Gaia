using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FoodPresenceDetector : MonoBehaviour
{
    public Food food;
    public GameObject foodPrefab;
    bool needSpawn;

    private void Start()
    {
        Instantiate(foodPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Beef") || other.CompareTag("Saumon") || other.CompareTag("Chicken") || other.CompareTag("Porc"))
        {
            needSpawn = true;
        }
    }

    public void SpawnFood()
    {
        if(needSpawn)
        {
            Instantiate(foodPrefab, transform.position, Quaternion.identity);
            needSpawn = false;
        }
    }

    public enum Food
    {
        beef,
        saumon,
        porc,
        chicken
    }
}
