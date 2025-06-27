using Assets.Gaia.Scripts.Garage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonManager : MonoBehaviour
{
    [SerializeField]
    private ComponentManager _componentManager;
    [SerializeField]
    private Door _door;
    [SerializeField]
    private TurnAnchor _turnAnchor;
    [SerializeField]
    private CarMovement _carMovement;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _positiveClip, _negativeClip;

    public void Validate()
    {
        if (_componentManager.GetComponents()[0] &&
            _componentManager.GetComponents()[1] &&
            _componentManager.GetComponents()[2])
        {
            _carMovement.StartCarMovement();
            _door.OpenDoor();
            _turnAnchor.ApplyTurn();
            _audioSource.PlayOneShot(_positiveClip, 1);
        }
        else
        {
            _audioSource.PlayOneShot(_negativeClip, 1);
        }
    }
}
