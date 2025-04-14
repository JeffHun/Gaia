using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField]
    Image _manufactureImg, _useImg, _recycleImg;

    int _manufacture, _use, _recycle;

    public void SetValues(int manufacture, int use, int recycle)
    {
        _manufacture = manufacture;
        _use = use;
        _recycle = recycle;

        SetBar();
    }

    void SetBar()
    {
        float maxRec = GetComponent<RectTransform>().rect.width;

        int total = _manufacture + _use + _recycle;

        _manufactureImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_manufacture * maxRec) / total, _manufactureImg.GetComponent<RectTransform>().rect.height);
        _useImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_use * maxRec) / total, _useImg.GetComponent<RectTransform>().rect.height);
        _recycleImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_recycle * maxRec) / total, _recycleImg.GetComponent<RectTransform>().rect.height);
    }
}
