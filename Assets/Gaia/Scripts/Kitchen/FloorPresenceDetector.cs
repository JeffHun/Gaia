using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPresenceDetector : MonoBehaviour
{
    public GameObject knifeRelocationPoint;
    public GameObject plateRelocationPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SliceObject>())
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.transform.position = knifeRelocationPoint.transform.position;
        }

        if (other.gameObject.GetComponent<MeatPresenceDetector>())
        {
            other.gameObject.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.transform.parent.transform.rotation = Quaternion.identity;
            other.gameObject.transform.parent.transform.position = plateRelocationPoint.transform.position;
        }
    }
}
