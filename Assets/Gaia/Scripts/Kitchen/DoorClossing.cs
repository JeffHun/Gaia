using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClossing : MonoBehaviour
{
    public float rotationSpeed = 100f;
    bool isHeld = false;

    public List<FoodPresenceDetector> foodPresenceDetectors = new List<FoodPresenceDetector>();

    public void IsHeld(bool status)
    {
        isHeld = status;
    }

    void Update()
    {
        if(!isHeld)
        {
            float currentYAngle = transform.eulerAngles.y;

            float angleToRotate = Mathf.DeltaAngle(currentYAngle, 0);

            if (Mathf.Abs(angleToRotate) > 0.1f)
            {
                float rotationStep = rotationSpeed * Time.deltaTime;
                float rotationAmount = Mathf.Clamp(rotationStep, 0, Mathf.Abs(angleToRotate));
                transform.Rotate(0f, rotationAmount * Mathf.Sign(angleToRotate), 0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                foreach(FoodPresenceDetector detector in foodPresenceDetectors)
                {
                    detector.SpawnFood();
                }
            }
        }
    }
}
