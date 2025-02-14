using Scenarios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetWall : DynamicEnvItem
{
    [SerializeField]
    private Material _vegetWallMatA, _vegetWallMatB, _vegetWallMatC, _vegetWallMatD;
    [SerializeField]
    private GameObject _vegetWallLeavesA, _vegetWallLeavesB, _vegetWallLeavesC, _vegetWallLeavesD;

    public override void ChangeLook(Scenario scenario)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        GameObject currentLeaves = null;
        MeshRenderer meshRend = transform.GetComponent<MeshRenderer>();

        switch (scenario)
        {
            case Scenario.scenarioA:
                meshRend.material = _vegetWallMatA;
                currentLeaves = Instantiate(_vegetWallLeavesA, transform.position, transform.rotation);
                break;
            case Scenario.scenarioB:
                meshRend.material = _vegetWallMatB;
                currentLeaves = Instantiate(_vegetWallLeavesB, transform.position, transform.rotation);
                break;
            case Scenario.scenarioC:
                meshRend.material = _vegetWallMatC;
                currentLeaves = Instantiate(_vegetWallLeavesC, transform.position, transform.rotation);
                break;
            case Scenario.scenarioD:
                meshRend.material = _vegetWallMatD;
                currentLeaves = Instantiate(_vegetWallLeavesD, transform.position, transform.rotation);
                break;
            case Scenario.scenarioE:
                meshRend.sharedMaterials = new Material[0];
                break;
        }
        if(currentLeaves != null)
        {
            currentLeaves.transform.SetParent(transform);

            // Change z rotation and x,y scale to create randomness
            int rotationZ = Random.Range(1,5) * 90;
            currentLeaves.transform.Rotate(0, 0, rotationZ, Space.Self);
            int signX = Random.value < 0.5f ? -1 : 1;
            int signY = Random.value < 0.5f ? -1 : 1;
            currentLeaves.transform.localScale = new Vector3(signX, signY, 1);
        }
    }
}
