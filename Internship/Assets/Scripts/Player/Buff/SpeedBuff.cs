using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedBuff : MonoBehaviour
{
    public float duration = 10;
    public Coroutine timer;
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TankBuff"))
        {
            player = other.transform.parent.parent.GetComponent<Player>();
            if (player != null)
            {
                player.buffs[buff.speed].DoBuff(duration);
                ObjectPool.Instance.PushObject(gameObject);
            }
        }
    }
}

public class speed : Ibuff
{
    public Player player;
    public Coroutine timer;
    public float coefficient = 2;
    public speed(Player FSM)
    {
        player = FSM;
    }

    public void DoBuff(float duration)
    {
        timer = player.StartCoroutine(speedBuff(duration));
    }

    IEnumerator speedBuff(float duration)
    {
        if (player != null)
        {
            player.currentSpeed = player.initSpeed * coefficient;
        }
        yield return new WaitForSeconds(duration);
        player.currentSpeed = player.initSpeed;
    }
    public void StopBuff()
    {
        if (player != null)
        {
            player.currentSpeed = player.initSpeed;
            if(timer!=null)
                player.StopCoroutine(timer);
        }
    }
}
