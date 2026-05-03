using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveWeapon : Weapon
{
    GameObject temp;

    public void Shoot(Transform shootPos, Transform playerUp, GameObject soundWavePrefab,string owner,string[] targets)
    {
        temp = ObjectPool.Instance.GetObject(soundWavePrefab);
        temp.transform.position = shootPos.position;
        temp.transform.rotation = playerUp.rotation;
        temp.GetComponent<SoundWaveProject>().Initialize(shootPos.position, playerUp.forward, owner, targets);
    }
}
