using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weapon {
    normalWeapon,doubleWeapon,
}

public interface Weapon 
{
    public void Shoot(Transform Up, Transform shootPos, GameObject bullet)
    {

    }

    public void Shoot(Transform Up, Transform shootPos_1,Transform shootPos_2 ,GameObject bullet)
    {

    }
}
