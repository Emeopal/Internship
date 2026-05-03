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
        normalBullet_Player = temp.GetComponent<NormalBullet_Player>();
        normalBullet_Player.enemy = enemy;
        normalBullet_Player.startPos = shootPos_1;
        temp.transform.rotation = Up.transform.rotation;
        normalBullet_Player.reboundCount = times;
        temp.GetComponent<Rigidbody>().AddForce(shootPos_1.forward * fireForce, ForceMode.Impulse);
        temp = ObjectPool.Instance.GetObject(bullet);
        normalBullet_Player = temp.GetComponent<NormalBullet_Player>();
        normalBullet_Player.startPos = shootPos_2;
        normalBullet_Player.enemy = enemy;
        temp.transform.rotation = Up.transform.rotation;
        normalBullet_Player.reboundCount = times;
        temp.GetComponent<Rigidbody>().AddForce(shootPos_2.forward * fireForce, ForceMode.Impulse);

    }
}
