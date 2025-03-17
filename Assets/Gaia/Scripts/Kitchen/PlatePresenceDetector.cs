using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePresenceDetector : MonoBehaviour
{
    [SerializeField] WeightScale scale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WeighScalePlate")
        {
            scale.SetIsPlatePresence(true);
            scale.UpdateCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WeighScalePlate")
        {
            scale.SetIsPlatePresence(false);
            scale.UpdateCanvas();
        }
    }
}
