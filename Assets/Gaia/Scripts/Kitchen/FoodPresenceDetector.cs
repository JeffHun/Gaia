using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FoodPresenceDetector : MonoBehaviour
{
    public Food food;
    public GameObject foodPrefab;
    bool needSpawn;
    [SerializeField]
    GameObject _fridge;

    private void Start()
    {
        Instantiate(foodPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Beef") || other.CompareTag("Saumon") || other.CompareTag("Chicken") || other.CompareTag("Porc") || other.CompareTag("Veget"))
        {
            needSpawn = true;
        }
    }

    public void SpawnFood()
    {
        if(needSpawn)
        {
            GameObject meat = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            meat.transform.parent = _fridge.transform;
            needSpawn = false;
        }
    }

    public enum Food
    {
        beef,
        saumon,
        porc,
        chicken,
        veget
    }
}
