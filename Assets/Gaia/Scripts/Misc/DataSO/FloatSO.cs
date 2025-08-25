using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float _value;
    [SerializeField]
    private float _maxValue;

    public float Value
    {
        get => _value; set => _value = value;
    }

    public float MaxValue { 
        get => _maxValue; set => _maxValue = value; 
    }
}