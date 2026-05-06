using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LaserWeapon : Weapon
{
    Vector3 hitPoint;
    GameObject hitObject;
    int damage = 3;
    public void Shoot(LineRenderer laser,Transform shootPos,Transform playerUp,LayerMask targetLayer, LayerMask enemy )
    {
        //伤害效果
        RaycastHit[] hits = Physics.RaycastAll(shootPos.position,playerUp.forward, 30);

        if (hits.Length > 0)
        {
            Transform temp = null;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Wall"))
                {
                    temp = hits[i].transform;
                }
                TryDealDamage(hits[i].collider.gameObject,temp,shootPos);
            }
        }

        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(shootPos.position , playerUp.forward, out hitInfo, 30, targetLayer);
        if (isHit)
        {
            hitPoint = hitInfo.point;     
        }
        else
        {
            hitPoint = shootPos.position + playerUp.forward * 30;
        }
        if (laser == null) return;

        laser.enabled = true;
        laser.SetPosition(0, shootPos.position);
        laser.SetPosition(1, hitPoint);
        MonoBehaviour mono = playerUp.GetComponent<MonoBehaviour>();
        if (mono != null)
        {
            mono.StartCoroutine(LaserTimer(laser));
        }
    }
    IEnumerator LaserTimer(LineRenderer laser)
    {
        yield return new WaitForSeconds(.15f);
        laser.enabled = false;
    }
    private void TryDealDamage(GameObject target,Transform temp,Transform shootPos)
    {
        if (target == null) return ;
        FSM fsm;
        if (target.TryGetComponent<FSM>(out fsm))
        {
            if(temp!=null && Vector3.Distance(shootPos.position,target.transform.position)<
                Vector3.Distance(shootPos.position, temp.transform.position))
            fsm.OnHurt(damage);
        }

        Player player;
        if (target.TryGetComponent<Player>(out player))
        {
            if (temp != null && Vector3.Distance(shootPos.position, target.transform.position) <
                Vector3.Distance(shootPos.position, temp.transform.position))
                player.OnHurt(damage);
        }
    }
}
