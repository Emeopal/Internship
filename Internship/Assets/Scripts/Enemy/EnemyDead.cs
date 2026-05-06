using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : Istate
{
    public FSM fsm;
    public GameManager gameManager;

    public EnemyDead(FSM enemyState)
    {
        fsm = enemyState;
        gameManager = Camera.main.GetComponent<GameManager>();
    }
    
    public void OnEnter()
    {
        Debug.Log("Ω¯»ÎÀ¿Õˆ");

        gameManager.levels[gameManager.currentLevel-1].EnemyCount -= 1;
        GameObject temp;
        temp=ObjectPool.Instance.GetObject(fsm.corpse);
        temp.transform.position = new Vector3(fsm.transform.position.x, 2, fsm.transform.position.z);
        ObjectPool.Instance.PushObject(fsm.transform.parent.gameObject);
    }
}
