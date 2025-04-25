using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGradient : MonoBehaviour
{
    [SerializeField] private Gradient _greenGradient, _redGradiant;
    [SerializeField] private Image _rightSide;
    [SerializeField] private Image _leftSide;


    public void SetBar(float value, float maxValue)
    {
        _leftSide.GetComponent<Image>().fillAmount = 0f;
        _rightSide.GetComponent<Image>().fillAmount = 0f;

        if (value > 0)
        {
            maxValue = Mathf.Abs(maxValue);
            _rightSide.GetComponent<Image>().fillAmount = value / maxValue;
            _rightSide.color = _redGradiant.Evaluate(_rightSide.fillAmount);
        }
        else
        {
            value = Mathf.Abs(value);
            maxValue = Mathf.Abs(maxValue);
            _leftSide.GetComponent<Image>().fillAmount = value / maxValue;
            _leftSide.color = _greenGradient.Evaluate(_leftSide.fillAmount);
        }
    }
}
