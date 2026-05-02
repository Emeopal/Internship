using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : Istate
{
    public FSM fsm;

    public EnemyDead(FSM enemyState)
    {
        fsm = enemyState;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
