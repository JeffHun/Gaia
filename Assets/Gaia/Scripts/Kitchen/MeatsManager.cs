using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeatsManager : MonoBehaviour
{
    List<GameObject> beefs = new List<GameObject>();
    List<GameObject> saumons = new List<GameObject>();
    List<GameObject> porcs = new List<GameObject>();
    List<GameObject> chickens = new List<GameObject>();
    List<GameObject> vegets = new List<GameObject>();

    public GameObject originalBeef;
    public GameObject originalSaumon;
    public GameObject originalPorc;
    public GameObject originalChicken;
    public GameObject originalVeget;

    public GameObject beefsParent;
    public GameObject saumonsParent;
    public GameObject porcsParent;
    public GameObject chickensParent;
    public GameObject vegetsParent;

    public GameObject spawnPoint;

    [SerializeField] WeightScale scale;

    public void SpawnBeef()
    {
        GameObject aBeef = Instantiate(originalBeef, spawnPoint.transform.position, Quaternion.identity);
        AddBeef(aBeef);
    }

    public void SpawnSaumon()
    {
        GameObject aSaumon = Instantiate(originalSaumon, spawnPoint.transform.position, Quaternion.identity);
        AddSaumon(aSaumon);
    }

    public void SpawnPorc()
    {
        GameObject aPorc = Instantiate(originalPorc, spawnPoint.transform.position, Quaternion.identity);
        AddPorc(aPorc);
    }

    public void SpawnChickens()
    {
        GameObject aChicken = Instantiate(originalChicken, spawnPoint.transform.position, Quaternion.identity);
        AddChicken(aChicken);
    }

    public void SpawnVeget()
    {
        GameObject aVeget = Instantiate(originalVeget, spawnPoint.transform.position, Quaternion.identity);
        AddVeget(aVeget);
    }

    public void AddBeef(GameObject aBeef)
    {
        beefs.Add(aBeef);
        ParentedBeef(aBeef);
    }

    public void AddSaumon(GameObject aSaumon)
    {
        saumons.Add(aSaumon);
        ParentedSaumon(aSaumon);
    }

    public void AddPorc(GameObject aPorc)
    {
        porcs.Add(aPorc);
        ParentedPorc(aPorc);
    }

    public void AddChicken(GameObject aChicken)
    {
        chickens.Add(aChicken);
        ParentedChicken(aChicken);
    }

    public void AddVeget(GameObject aVeget)
    {
        vegets.Add(aVeget);
        ParentedVeget(aVeget);
    }

    public void ParentedBeef(GameObject aBeef)
    {
        aBeef.transform.parent = beefsParent.transform;
    }

    public void ParentedSaumon(GameObject aSaumon)
    {
        aSaumon.transform.parent = saumonsParent.transform;
    }

    public void ParentedPorc(GameObject aPorc)
    {
        aPorc.transform.parent = porcsParent.transform;
    }

    public void ParentedChicken(GameObject aChicken)
    {
        aChicken.transform.parent = chickensParent.transform;
    }

    public void ParentedVeget(GameObject aVeget)
    {
        aVeget.transform.parent = vegetsParent.transform;
    }

    public void DestroyAllMeats()
    {
        for (int i = 0; i < beefs.Count; i++)
        {
            Destroy(beefs[i]);
        }
        beefs.Clear();


        for (int i = 0; i < saumons.Count; i++)
        {
            Destroy(saumons[i]);
        }
        saumons.Clear();


        for (int i = 0; i < porcs.Count; i++)
        {
            Destroy(porcs[i]);
        }
        porcs.Clear();


        for (int i = 0; i < chickens.Count; i++)
        {
            Destroy(chickens[i]);
        }
        chickens.Clear();

        for (int i = 0; i < vegets.Count; i++)
        {
            Destroy(vegets[i]);
        }
        vegets.Clear();

        scale.Reset();
    }
}
