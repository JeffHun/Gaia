using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] GameObject _cloudPrefab;
    [SerializeField] Transform _cloudPosA;
    [SerializeField] Transform _cloudPosB;
    [SerializeField] float _angle = 5.0f;
    [SerializeField] float _biggestScale = 2.0f;

    GameObject _cloudA, _cloudB, _cloudC, _cloudD;
    float _maxFootprint;
    float _currentFootprint;
    bool _isAnimate = false;

    public void GenerateCloud(float currentFooprint, float maxFooprint)
    {
        _currentFootprint = currentFooprint;
        _maxFootprint = maxFooprint;

        _cloudA = Instantiate(_cloudPrefab, _cloudPosA.transform.position, Quaternion.identity);
        _cloudA.transform.parent = _cloudPosA;
        _cloudB = Instantiate(_cloudPrefab, _cloudPosA.transform.position, Quaternion.identity);
        _cloudB.transform.parent = _cloudPosA;

        float scale = (_currentFootprint * _biggestScale) / _maxFootprint;

        _cloudA.transform.localScale = new Vector3(scale, scale, scale);
        _cloudB.transform.localScale = new Vector3(scale, scale, scale);

        _cloudC = Instantiate(_cloudPrefab, _cloudPosB.transform.position, Quaternion.identity);
        _cloudC.transform.parent = _cloudPosB;
        _cloudD = Instantiate(_cloudPrefab, _cloudPosB.transform.position, Quaternion.identity);
        _cloudD.transform.parent = _cloudPosB;

        _cloudC.transform.localScale = new Vector3(_biggestScale, _biggestScale, _biggestScale);
        _cloudD.transform.localScale = new Vector3(_biggestScale, _biggestScale, _biggestScale);

        _isAnimate = true;
    }

    void Update()
    {
        if (_isAnimate)
        {
            _cloudA.transform.Rotate(Vector3.up, _angle / 3);
            _cloudA.transform.Rotate(Vector3.forward, _angle / 2);
            _cloudA.transform.Rotate(Vector3.right, _angle);
            _cloudB.transform.Rotate(Vector3.down, _angle);
            _cloudB.transform.Rotate(Vector3.back, _angle / 2);
            _cloudB.transform.Rotate(Vector3.left, _angle / 3);

            _cloudC.transform.Rotate(Vector3.up, _angle / 3);
            _cloudC.transform.Rotate(Vector3.forward, _angle / 2);
            _cloudC.transform.Rotate(Vector3.right, _angle);
            _cloudD.transform.Rotate(Vector3.down, _angle);
            _cloudD.transform.Rotate(Vector3.back, _angle / 2);
            _cloudD.transform.Rotate(Vector3.left, _angle / 3);
        }
    }
}
