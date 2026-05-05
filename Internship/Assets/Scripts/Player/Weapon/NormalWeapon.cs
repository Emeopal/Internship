using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeapon : Weapon
{
    GameObject temp;
    public float fireForce = 35;
    public NormalWeapon()
    {

    }
    public void Shoot(Transform Up, Transform shootPos, GameObject bullet, int times)
    {
        temp = ObjectPool.Instance.GetObject(bullet);
        temp.transform.rotation = Up.transform.rotation;

        NormalBullet_Player playerBullet;
        if (temp.TryGetComponent<NormalBullet_Player>(out playerBullet))
        {
            playerBullet.startPos = shootPos;
            playerBullet.reboundCount = times;
            playerBullet.prepareAngel = Up.transform.eulerAngles;
            playerBullet.SetVelocity(Up.transform.forward * fireForce);
        }
        else
        {
            NormalBullet_Enemy enemyBullet = temp.GetComponent<NormalBullet_Enemy>();
            if (enemyBullet != null)
            {
                enemyBullet.startPos = shootPos;
                enemyBullet.reboundCount = times;
                enemyBullet.prepareAngel = Up.transform.eulerAngles;
                enemyBullet.SetVelocity(Up.transform.forward * fireForce);
            }
        }
    }
}
