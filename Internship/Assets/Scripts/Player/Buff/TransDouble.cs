using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransDouble : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out player))
        {
            player.TransWeapon(weapon.doubleWeapon);
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
