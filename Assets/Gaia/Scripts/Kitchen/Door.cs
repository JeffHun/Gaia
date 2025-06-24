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

    bool _isMoving;
    bool _isOpen = false;
    bool _isCount;
    float _timer;

    Quaternion _startRotation;
    Quaternion _endRotation;

    public void OpenDoor()
    {
        if (!_isOpen)
        {
            _isMoving = true;
            _isOpen = true;
            _timer = 0;
            _startRotation = Quaternion.identity;
            _endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90f, 0));
            //ScenesManager.Instance.SwitchAsyncSceneAuto();
        }
    }

    public void CloseDoor()
    {
        if (_isOpen)
        {
            _isMoving = true;
            _isOpen = false;
            _timer = 0;
            _endRotation = Quaternion.identity;
            _startRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90f, 0));
            if (ScenesManager.Instance)
                ScenesManager.Instance.SwitchAsyncSceneAuto();
        }
    }

    public void StartCountDown()
    {
        if (_isOpen)
            _isCount = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _timer += Time.deltaTime;
            float t = _timer / _delayDoor;
            _door.transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, t);
            if (t >= 1f)
            {
                _isMoving = false;
                _timer = _delayTP;
            }
        }
        if (_isCount)
        {
            _timer -= Time.deltaTime;
            _counterTxt.text = Mathf.Round(_timer).ToString();
            if (_timer <= 0)
            {
                if (ScenesManager.Instance)
                    ScenesManager.Instance.SwitchAsyncSceneAuto();
            }
        }
    }

}
