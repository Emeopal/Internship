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
    public void Shoot(Transform Up,Transform shootPos,GameObject bullet,string enemy,int times)
    {
        NormalBullet_Player normalBullet_Player;
        temp = ObjectPool.Instance.GetObject(bullet);
        normalBullet_Player = temp.GetComponent<NormalBullet_Player>();
        normalBullet_Player.startPos = shootPos;
        temp.transform.rotation = Up.transform.rotation;
        normalBullet_Player.enemy = enemy;
        normalBullet_Player.reboundCount = times;
        temp.GetComponent<Rigidbody>().AddForce(shootPos.forward * fireForce, ForceMode.Impulse);
        
    }
}
