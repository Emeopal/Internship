using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    public float duration;
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
                player.buffs[buff.shield].DoBuff(duration);
                ObjectPool.Instance.PushObject(gameObject);
            }
        }
    }
}

public class shield : Ibuff
{
    public Player player;
    public Coroutine timer;

    public shield(Player FSM)
    {
        player = FSM;
    }

    public void DoBuff(float duration)
    {
        timer = player.StartCoroutine(shieldBuff(duration));
        if (player.coolHurtTimer != null)
        {
            player.StopCoroutine(player.coolHurtTimer);
        }
    }

    IEnumerator shieldBuff(float duration)
    {
        if (player != null)
        {
            player.canBeHurt = false;
            //这里要阻止受伤的协程
        }
        yield return new WaitForSeconds(duration);
        player.canBeHurt = true;
    }

    public void StopBuff()
    {
        if (player != null)
        {
            player.canBeHurt = true;
            if (timer!=null)
                player.StopCoroutine(timer);
        }
    }
}
