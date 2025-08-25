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

    public ScenesManager _scenesManager;

    [SerializeField]
    FloatSO _score;

    public List<string> _currentMeats = new List<string>();

    bool _isWeight, _isMeat;

    private void OnEnable()
    {
        if (_scenesManager)
            _scenesManager.OnSceneChange.AddListener(ScoreManage);
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

        if (totalWeight <= 5420)
            _score.Value += 0;
        else if (totalWeight <= 6013)
            _score.Value += .25f;
        else if (totalWeight <= 6606)
            _score.Value += .5f;
        else if (totalWeight <= 7200)
            _score.Value += .75f;
        else
            _score.Value += 1f;
    }

    private void LateUpdate()
    {
        if (_isWeight && _isMeat)
            _door.OpenDoor();
        else
            _door.CloseDoor();
    }
}
