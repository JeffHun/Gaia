using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] Transform _startPoint, _endPoint;
    [SerializeField] float _duration = 5f;
    [SerializeField] CloudManager _cloudManager;
    [SerializeField] UIManager _UIManager;
    [SerializeField] ComponentManager _componentManager;
    bool _isMoving = false;
    float _elapsedTime = 0f;

    public void StartCarMovement()
    {
        _elapsedTime = 0f;
        _isMoving = true;
    }

    
    void Update()
    {
        if(_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsedTime / _duration);
            transform.position = Vector3.Lerp(_startPoint.position, _endPoint.position, t);

            if (t >= 1f)
            {
                _isMoving = false;
                _cloudManager.GenerateCloud(_componentManager.GetTotalFootPrint(), _UIManager.GetMaxCarFootprint());
            }
        }
    }
}
