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

    public void CheckPlate()
    {
        _isWeight = false;
        _isMeat = false;

        _isWeight = _plates.All(plate => plate.GetWeight() > 0);

        if (_isWeight)
            CheckMeatPlates();

        if (_isWeight && _isMeat)
            _door.OpenDoor();
        else
            _door.CloseDoor();
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
}
