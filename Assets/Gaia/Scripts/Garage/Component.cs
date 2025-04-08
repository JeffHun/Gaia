using categories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace categories
{
    public enum Category
    {
        type,
        engine,
        option
    }
}

public class Component : MonoBehaviour
{
    Category _category;
    string _name;
    int _price;
    int _manufactureFootprint;
    int _useFootprint;
    int _recycleFootprint;
    Image _img;

    public Category getCategory()
    {
        return _category;
    }

    public string getName()
    {
        return _name;
    }

    public int getPrice()
    {
        return _price;
    }

    public int getManufactureFootprint()
    {
        return _manufactureFootprint;
    }

    public int getUseFootprint()
    {
        return _useFootprint;
    }

    public int getRecycleFootprint()
    {
        return _recycleFootprint;
    }

    public Image getImg()
    {
        return _img;
    }
}
