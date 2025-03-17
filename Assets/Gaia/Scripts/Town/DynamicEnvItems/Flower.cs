using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum FlowerType
{
    weak,
    normal,
    strong
}

public class Flower : DynamicEnvItem
{
    [SerializeField]
    GameObject _flowerPrefab;

    [SerializeField]
    FlowerType _type;

    List<GameObject> _flowers = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
            _flowers.Add(transform.GetChild(i).gameObject);

        foreach(GameObject flower in _flowers)
        {
            int rotY = Random.Range(0,365);
            flower.transform.Rotate(0, rotY, 0, Space.Self);
            float scale = Random.Range(1,1.5f);
            flower.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public override void ChangeLook(Scenario scenario)
    {
        foreach (GameObject flower in _flowers)
            if (flower.transform.childCount >0)
                Destroy(flower.transform.GetChild(0).gameObject);

        // Instantiate 100,70,50,30 or 0% of flowers depending ont the scenario and flower strenght
        int i = 0;
        switch (scenario)
        {
            case Scenario.scenarioA:
                for(i = 0; i < _flowers.Count; ++i)
                    Instantiate(_flowerPrefab, _flowers[i].transform);
                break;
            case Scenario.scenarioB:
                i = (30 * _flowers.Count) / 100;
                for (; i < _flowers.Count; ++i)
                    Instantiate(_flowerPrefab, _flowers[i].transform);
                break;
            case Scenario.scenarioC:
                if (_type == FlowerType.weak)
                    break;
                i = (60 * _flowers.Count) / 100;
                for (; i < _flowers.Count; ++i)
                    Instantiate(_flowerPrefab, _flowers[i].transform);
                break;
            case Scenario.scenarioD:
                if (_type == FlowerType.weak || _type == FlowerType.normal)
                    break;
                i = (90 * _flowers.Count) / 100;
                for (; i < _flowers.Count; ++i)
                    Instantiate(_flowerPrefab, _flowers[i].transform);
                break;
        }
    }
}
