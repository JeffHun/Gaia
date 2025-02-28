using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarColor : MonoBehaviour
{
    [SerializeField]
    List<Color32> _colors = new List<Color32>();

    [SerializeField]
    string targetMaterialName = "Car_body";

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Material[] materials = renderer.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name.Contains(targetMaterialName))
                {
                    if(_colors.Count > 0)
                    {
                        int rand = Random.Range(0, _colors.Count);
                        materials[i].color = _colors[rand];
                        renderer.materials = materials;
                    }
                    return;
                }
            }
        }
    }
}
