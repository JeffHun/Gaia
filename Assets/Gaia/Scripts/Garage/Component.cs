using categories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace categories
{
    public enum Category
    {
        Type,
        Moteur,
        Options
    }
}

public class Component
{
    int _id;
    Category _category;
    string _name;
    int _price;
    int _manufactureFootprint;
    int _useFootprint;
    int _recycleFootprint;
    Sprite _img;

    public Component(int id, Category category, string name, int price, int manufactureFootprint, int useFootprint, int recycleFootprint, Sprite img)
    {
        _id = id;
        _category = category;
        _name = name;
        _price = price;
        _manufactureFootprint = manufactureFootprint;
        _useFootprint = useFootprint;
        _recycleFootprint = recycleFootprint;
        _img = img;
    }

    public int GetId()
    {
        return _id;
    }

    public Category GetCategory()
    {
        return _category;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetPrice()
    {
        return _price;
    }

    public int GetManufactureFootprint()
    {
        return _manufactureFootprint;
    }

    public int GetUseFootprint()
    {
        return _useFootprint;
    }

    public int GetRecycleFootprint()
    {
        return _recycleFootprint;
    }

    public Sprite GetImg()
    {
        return _img;
    }
}
