
using UnityEngine;

namespace Assets.Gaia.Scripts.Garage
{
    public class TurnAnchor : MonoBehaviour
    {
        public void ApplyTurn()
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y + 180, rotation.z);
            transform.rotation = Quaternion.Euler(rotation);
            Debug.Log(transform.rotation.eulerAngles);
        }
    }
}