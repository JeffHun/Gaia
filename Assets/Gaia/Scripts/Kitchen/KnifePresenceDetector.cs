using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KnifePresenceDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {
            other.gameObject.GetComponent<SliceObject>().enabled = false;
            other.gameObject.transform.GetChild(1).GetComponent<MeshCollider>().enabled = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {
            other.gameObject.GetComponent<SliceObject>().enabled = true;
            other.gameObject.transform.GetChild(1).GetComponent<MeshCollider>().enabled = true;
        }
    }
}
