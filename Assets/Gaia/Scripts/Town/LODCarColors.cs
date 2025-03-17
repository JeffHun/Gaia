using System.Collections.Generic;
using UnityEngine;

public class LODCarColors : MonoBehaviour
{

    [SerializeField]
    List<Color32> _colors = new List<Color32>();

    [SerializeField]
    string targetMaterialName = "Car_body";

    LODGroup _lodGroup;

    void Start()
    {
        _lodGroup = GetComponent<LODGroup>();

        if( _lodGroup != null )
        {
            int rand = Random.Range(0, _colors.Count);

            LOD[] lods = _lodGroup.GetLODs();

            foreach (LOD lod in lods)
            {
                foreach (Renderer renderer in lod.renderers)
                {
                    Material[] materials = renderer.materials;

                    for (int i = 0; i < materials.Length; i++)
                    {
                        if (materials[i].name.Contains(targetMaterialName))
                        {
                            if (_colors.Count > 0)
                            {
                                materials[i].color = _colors[rand];
                                renderer.materials = materials;
                            }
                        }
                    }
                }
            }
        }


        Renderer[] renderers = GetComponentsInChildren<Renderer>(includeInactive: true);

        if (renderers != null)
        {
            
        }
    }
}