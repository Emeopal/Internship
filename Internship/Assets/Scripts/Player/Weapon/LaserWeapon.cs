using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
            foreach(RaycastHit hit in hits)
            {
                TryDealDamage(hit.collider.gameObject);
            }
        }

        //�Ӿ�Ч��
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(shootPos.position , playerUp.forward, out hitInfo, 30, targetLayer);
        if (isHit)
        {
            hitPoint = hitInfo.point;      // ���е�����
        }
        else
        {
            // δ���� - �յ�Ϊ�����봦
            hitPoint = shootPos.position + playerUp.forward * 30;
        }
        if (laser == null) return;

        // ���ü����������˵㣺�����յ�
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
    private void TryDealDamage(GameObject target)
    {
        if (target == null) return ;

        FSM temp;
        if (target.TryGetComponent<FSM>(out temp))
        {
            temp.OnHurt(damage);
        }

        Player player;
        if (target.TryGetComponent<Player>(out player))
        {
            player.OnHurt(damage);
        }
    }
}
