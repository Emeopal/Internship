using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class DoubleWeapon : Weapon
{
    GameObject temp;
    public float fireForce = 35;
    public void Shoot(Transform Up, Transform shootPos_1, Transform shootPos_2, GameObject bullet,string enemy,int times)
    {
        NormalBullet_Player normalBullet_Player;
        temp = ObjectPool.Instance.GetObject(bullet);
        temp.transform.rotation = Up.transform.rotation;

        if (temp.TryGetComponent<NormalBullet_Player>(out normalBullet_Player))
        {
            normalBullet_Player.startPos = shootPos_1;
            //normalBullet_Player.prepareAngel = Up.transform.eulerAngles;
            normalBullet_Player.reboundCount = times;
            normalBullet_Player.SetVelocity(shootPos_1.forward * fireForce);

            temp = ObjectPool.Instance.GetObject(bullet);
            normalBullet_Player = temp.GetComponent<NormalBullet_Player>();
            normalBullet_Player.startPos = shootPos_2;
            //normalBullet_Player.prepareAngel = Up.transform.eulerAngles;
            normalBullet_Player.reboundCount = times;
            normalBullet_Player.SetVelocity(shootPos_2.forward * fireForce);
        }
        else
        {
            NormalBullet_Enemy enemyBullet;
            if(temp.TryGetComponent<NormalBullet_Enemy>(out enemyBullet))
            {
                enemyBullet.startPos = shootPos_1;
                enemyBullet.reboundCount = times;
                enemyBullet.prepareAngel = Up.transform.eulerAngles;
                enemyBullet.SetVelocity(shootPos_1.forward * fireForce);

                temp = ObjectPool.Instance.GetObject(bullet);
                enemyBullet = temp.GetComponent<NormalBullet_Enemy>();
                enemyBullet.startPos = shootPos_2;
                enemyBullet.reboundCount = times;
                enemyBullet.prepareAngel = Up.transform.eulerAngles;
                enemyBullet.SetVelocity(shootPos_2.forward * fireForce);
            }
        }

    }
}
