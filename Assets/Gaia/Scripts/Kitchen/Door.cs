using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _counterTxt;

    [SerializeField]
    float _delayTP, _doorSpeed, _delayDoor;

    [SerializeField]
    GameObject _door;

    bool _isOpen;
    bool _isCount;
    float _timer;

    Quaternion _startRotation;
    Quaternion _endRotation;

    public void OpenDoor()
    {
        _isOpen = true;
        _startRotation = Quaternion.identity;
        _endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90f, 0));
        ScenesManager.Instance.SwitchAsyncSceneAuto();
    }

    public void StartCountDown()
    {
        if (_isOpen)
            _isCount = true;
    }

    private void Update()
    {
        if(_isOpen && !_isCount)
        {
            _timer += Time.deltaTime;
            float t = _timer / _delayDoor;
            _door.transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);
            if (t >= 1f)
            {
                _timer = _delayTP;
            }
        }
        if(_isCount)
        {
            _timer -= Time.deltaTime;
            _counterTxt.text = Mathf.Round(_timer).ToString();
            if (_timer <= 0)
            {
                _isCount = false;
                ScenesManager.Instance.LaunchScene();
            }
        }
    }

}
