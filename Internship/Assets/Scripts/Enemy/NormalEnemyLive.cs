using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : Istate
{
    public FSM fsm;
    public EnemyLive(FSM enemyState)
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
