using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Player player;

    private void OnEnable()
    {
        transform.eulerAngles = Vector3.zero;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TankBuff"))
        {
            player = other.transform.parent.parent.GetComponent<Player>();
            if (player != null)
            {
                player.currentReboundCount += 1;
                ObjectPool.Instance.PushObject(gameObject);
            }
        }
    }
}
