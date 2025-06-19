using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MeatReconstitution : MonoBehaviour
{
    public GameObject _reconstitutionBtn;

    [SerializeField]
    RawImage _meatIcon;

    [SerializeField]
    GameObject _originalBeef, _originalSaumon, _originalPorc, _originalChicken, _originalVeget, _spawnPoint;

    [SerializeField]
    Texture _beefTex, _saumonTex, _porcTex, _chickenTex, _vegetTex;

    [SerializeField]
    List<GameObject> _meats = new List<GameObject>();

    GameObject _lastMeat;

    private void Start()
    {
        _reconstitutionBtn.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Meat>(out Meat aMeat))
        {
            _meats.Add(other.gameObject);
            switch (aMeat.GetMeatType())
            {
                case Meat.MeatType.Beef:
                    _lastMeat = _originalBeef;
                    break;
                case Meat.MeatType.Saumon:
                    _lastMeat = _originalSaumon;
                    break;
                case Meat.MeatType.Porc:
                    _lastMeat = _originalPorc;
                    break;
                case Meat.MeatType.Chicken:
                    _lastMeat = _originalChicken;
                    break;
                case Meat.MeatType.Veget:
                    _lastMeat = _originalVeget;
                    break;
            }
            ChangeIcon(aMeat.GetMeatType());
        }

        _reconstitutionBtn.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Meat>(out Meat aMeat))
            _meats.Remove(other.gameObject);
    }

    void ChangeIcon(Meat.MeatType aType)
    {
        switch (aType)
        {
            case Meat.MeatType.Beef:
                _meatIcon.texture = _beefTex;
                break;
            case Meat.MeatType.Saumon:
                _meatIcon.texture = _saumonTex;
                break;
            case Meat.MeatType.Porc:
                _meatIcon.texture = _porcTex;
                break;
            case Meat.MeatType.Chicken:
                _meatIcon.texture = _chickenTex;
                break;
            case Meat.MeatType.Veget:
                _meatIcon.texture = _vegetTex;
                break;
        }
    }

    public void SpawnLastMeat()
    {
        GameObject aVeget = Instantiate(_lastMeat, _spawnPoint.transform.position, Quaternion.identity);
        foreach (GameObject meat in _meats)
            Destroy(meat);
    }
}
