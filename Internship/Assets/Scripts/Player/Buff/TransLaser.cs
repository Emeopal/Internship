using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransLaser : MonoBehaviour
{
    public Player player;

    private void OnEnable()
    {
        transform.eulerAngles = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out player))
        {
            player.TransWeapon(weapon.laserWeapon);
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
