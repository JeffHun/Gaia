using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (_componentManager.GetComponents()[0] &&
            _componentManager.GetComponents()[1] &&
            _componentManager.GetComponents()[2])
        {
            _elapsedTime = 0f;
            _isMoving = true;
            string text = "Car : \nComponents: " +
                string.Join("\n\t", _componentManager.GetComponents().Select(c => $"{c.Name}")) + 
                "\nFootprint: " + 
                _componentManager.GetTotalFootprint() +"\nPrice: " + 
                _componentManager.GetTotalPrice() + "\n";
            if(FileLogsManager.Instance != null)
                FileLogsManager.Instance.LogToFile(text);
            if(ScenesManager.Instance != null)
            {
                float value = 0f;
                switch(_componentManager.CompositionChars)
                {
                    case "ECE":
                        value = 0;
                        break;
                    case "ECC":
                        value = 0.04f;
                        break;
                    case "ECS":
                        value = 0.08f;
                        break;
                    case "HCE":
                        value = 0.12f;
                        break;
                    case "HCC":
                        value = 0.15f;
                        break;
                    case "HCS":
                        value = 0.19f;
                        break;
                    case "TCE":
                        value = 0.23f;
                        break;
                    case "TCC":
                        value = 0.27f;
                        break;
                    case "TCS":
                        value = 0.31f;
                        break;
                    case "EBE":
                        value = 0.35f;
                        break;
                    case "EBC":
                        value = 0.38f;
                        break;
                    case "EBS":
                        value = 0.42f;
                        break;
                    case "HBE":
                        value = 0.46f;
                        break;
                    case "ESE":
                        value = 0.5f;
                        break;
                    case "HBC":
                        value = 0.54f;
                        break;
                    case "HBS":
                        value = 0.58f;
                        break;
                    case "ESC":
                        value = 0.62f;
                        break;
                    case "ESS":
                        value = 0.65f;
                        break;
                    case "HSE":
                        value = 0.69f;
                        break;
                    case "HSC":
                        value = 0.73f;
                        break;
                    case "HSS":
                        value = 0.77f;
                        break;
                    case "TBE":
                        value = 0.81f;
                        break;
                    case "TBC":
                        value = 0.85f;
                        break;
                    case "TBS":
                        value = 0.88f;
                        break;
                    case "TSE":
                        value = 0.92f;
                        break;
                    case "TSC":
                        value = 0.96f;
                        break;
                    case "TSS":
                        value = 1f;
                        break;
                }
                ScenesManager.Instance.UpdateScore(value);                
            }
        }
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
                _cloudManager.GenerateCloud(_componentManager.GetTotalFootprint(), _UIManager.GetMaxCarFootprint());
            }
        }
    }
}
