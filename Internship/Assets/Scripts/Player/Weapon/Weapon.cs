using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum weapon {
    normalWeapon,doubleWeapon,laserWeapon,soundWaveWeapon
}

public interface Weapon 
{
    public void Shoot(Transform Up, Transform shootPos, GameObject bullet,int times)
    {

    }

    public void Shoot(Transform Up, Transform shootPos_1,Transform shootPos_2 ,GameObject bullet,string enemy,int times )
    {

    }

    public void Shoot(LineRenderer laser, Transform shootPos, Transform playerUp, LayerMask targetLayer,LayerMask enemy)
    {

    }
    public void Shoot(Transform shootPos, Transform playerUp, GameObject soundWavePrefab)
    {

    }
}
