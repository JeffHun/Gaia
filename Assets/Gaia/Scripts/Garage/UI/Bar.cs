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
        _manufacture = manufacture >= 0 ? manufacture : 0;
        _use = use >= 0 ? use : 0;
        _recycle = recycle >= 0 ? recycle : 0;

        SetBar();
    }

    void SetBar()
    {
        float maxRec = GetComponent<RectTransform>().rect.width;

        int total = _manufacture + _use + _recycle;

        _manufactureImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_manufacture * maxRec) / total, _manufactureImg.GetComponent<RectTransform>().rect.height);
        _useImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_use * maxRec) / total, _useImg.GetComponent<RectTransform>().rect.height);
        _recycleImg.GetComponent<RectTransform>().sizeDelta = new Vector2((_recycle * maxRec) / total, _recycleImg.GetComponent<RectTransform>().rect.height);

        _manufactureImg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _useImg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _recycleImg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
