using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatesManager : MonoBehaviour
{
    [SerializeField]
    List<Plate> _plates = new List<Plate>();

    [SerializeField]
    List<string> _targetMeats = new List<string>() { "Porc", "Saumon", "Beef", "Chicken", "Veget" };

    [SerializeField]
    Door _door;

    public List<string> _currentMeats = new List<string>();

    bool _isWeight, _isMeat;

    private void OnEnable()
    {
        if (ScenesManager.Instance)
            ScenesManager.Instance.OnSceneChange.AddListener(ScoreManage);
    }

    public void CheckPlate()
    {
        _isWeight = false;
        _isMeat = false;

        _isWeight = _plates.All(plate => plate.GetWeight() > 0);

        if (_isWeight)
            CheckMeatPlates();

    }

    void CheckMeatPlates()
    {
        HashSet<string> foundMeats = new HashSet<string>();

        foreach (var plate in _plates)
        {
            foreach (var meat in plate.GetMeats())
            {
                foundMeats.Add(meat);
            }
        }

        _isMeat = _targetMeats.All(target => foundMeats.Contains(target));
    }
    
    void ScoreManage()
    {
        float totalWeight = 0;
        foreach(var plate in _plates)
        {
            totalWeight += plate.GetFootprint();
        }

        if(ScenesManager.Instance)
        {
            if (totalWeight <= 5420)
                ScenesManager.Instance.UpdateScore(0);
            else if (totalWeight <= 6013)
                ScenesManager.Instance.UpdateScore(.25f);
            else if (totalWeight <= 6606)
                ScenesManager.Instance.UpdateScore(.5f);
            else if (totalWeight <= 7200)
                ScenesManager.Instance.UpdateScore(.75f);
            else
                ScenesManager.Instance.UpdateScore(1f);
        }
    }

    private void LateUpdate()
    {
        if (_isWeight && _isMeat)
            _door.OpenDoor();
        else
            _door.CloseDoor();
    }
}
