using categories;
using Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComponentGhost : MonoBehaviour
{
    [SerializeField] GameObject _socket;
    [SerializeField] Material _ghostMat;

    GameObject _ghost;

    private void Start()
    {
        GameObject[] sockets = FindObjectsOfType<Socket>().Select(socket => socket.gameObject).ToArray();

        ComponentData componentData = GetComponent<ComponentData>();
        Category category = componentData.GetCategory();

        foreach (GameObject socket in sockets)
        {
            if(socket.GetComponent<Socket>().GetCategory() == category)
                _socket = socket;
        }

        _ghost = Instantiate(componentData.GetModel(), _socket.transform);
        Material[] mats = _ghost.GetComponent<Renderer>().materials;
        for (int i = 0; i < mats.Length; i++)
            mats[i] = _ghostMat;
        _ghost.GetComponent<Renderer>().materials = mats;

        _ghost.SetActive(false);

    }

    public void ActiveGhost(bool status)
    {
        _ghost.SetActive(status);
    }
}
