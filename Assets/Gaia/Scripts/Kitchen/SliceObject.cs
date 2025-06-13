using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObject : MonoBehaviour
{
    [SerializeField] Transform startSlicePtn;
    [SerializeField] Transform endSlicePtn;
    [SerializeField] VelocityEstimator velocityEstimator;
    [SerializeField] MeatsManager meatsManager;

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePtn.position, endSlicePtn.position, out RaycastHit hit);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            if(target.GetComponent<Meat>())
                Slice(target);
        }
    }

    void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePtn.position - startSlicePtn.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endSlicePtn.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, target.GetComponent<Meat>().GetCrossSectionMat());
            upperHull.name = target.name;
            SetUpSlicedComponent(upperHull, target.GetComponent<Meat>().GetMeatType(), target.GetComponent<Meat>().GetCrossSectionMat(), target.tag);

            GameObject lowerHull = hull.CreateLowerHull(target, target.GetComponent<Meat>().GetCrossSectionMat());
            lowerHull.name = target.name;
            SetUpSlicedComponent(lowerHull, target.GetComponent<Meat>().GetMeatType(), target.GetComponent<Meat>().GetCrossSectionMat(), target.tag);

            switch (target.GetComponent<Meat>().GetMeatType())
            {
                case Meat.MeatType.Beef:
                    meatsManager.AddBeef(upperHull);
                    meatsManager.AddBeef(lowerHull);
                    break;
                case Meat.MeatType.Saumon:
                    meatsManager.AddSaumon(upperHull);
                    meatsManager.AddSaumon(lowerHull);
                    break;
                case Meat.MeatType.Porc:
                    meatsManager.AddPorc(upperHull);
                    meatsManager.AddPorc(lowerHull);
                    break;
                case Meat.MeatType.Chicken:
                    meatsManager.AddChicken(upperHull);
                    meatsManager.AddChicken(lowerHull);
                    break;
                case Meat.MeatType.Veget:
                    meatsManager.AddVeget(upperHull);
                    meatsManager.AddVeget(lowerHull);
                    break;
            }
            Destroy(target);
        }
    }

    private void SetUpSlicedComponent(GameObject slicedObj, Meat.MeatType aMeatType, Material aCrossSectionMat, string aTag)
    {
        Rigidbody rb = slicedObj.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        MeshCollider col = slicedObj.AddComponent<MeshCollider>();
        col.convex = true;
        slicedObj.AddComponent<Meat>();
        slicedObj.GetComponent<Meat>().SetMeatType(aMeatType);
        slicedObj.GetComponent<Meat>().SetCrossSection(aCrossSectionMat);
        slicedObj.tag = aTag;

        XRGrabInteractable grabInteractable = slicedObj.AddComponent<XRGrabInteractable>();
        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        grabInteractable.useDynamicAttach = true;
    }
}
