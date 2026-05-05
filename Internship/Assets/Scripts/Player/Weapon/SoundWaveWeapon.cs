using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveWeapon : Weapon
{
    GameObject temp;

    public void Shoot(Transform shootPos, Transform playerUp, GameObject soundWavePrefab)
    {
        temp = ObjectPool.Instance.GetObject(soundWavePrefab);
        temp.transform.position = shootPos.position;
        temp.transform.rotation = playerUp.rotation;
        if(temp.TryGetComponent<SoundWaveProject>(out SoundWaveProject swp))
            swp.Initialize(shootPos.position, playerUp.forward);
        if(temp.TryGetComponent<SoundWave_Enemy>(out SoundWave_Enemy swe))
        {
            swe.Initialize(shootPos.position, playerUp.forward);
        }
    }
}
