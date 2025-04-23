using categories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private Category _category;

    public Category GetCategory() { return _category; }
}
