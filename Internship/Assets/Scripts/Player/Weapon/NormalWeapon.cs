using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeapon : Weapon
{
    GameObject temp;
    private Vector3 solidAngel = new Vector3(90, 0, 0);
    public float fireForce = 25;
    public NormalWeapon()
    {

    }
    public void Shoot(Transform Up,Transform shootPos,GameObject bullet)
    {
        temp = ObjectPool.Instance.GetObject(bullet);
        temp.transform.rotation = Up.transform.rotation;
        temp.transform.eulerAngles += solidAngel;
        temp.GetComponent<NormalBullet_Player>().startPos = shootPos;
        temp.GetComponent<Rigidbody>().AddForce(shootPos.forward * fireForce, ForceMode.Impulse);
    }
}
