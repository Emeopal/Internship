using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [Header("初始数值")]
    public int maxLife;
    public float initSpeed;

    [Header("当前数值")]
    public int currentLife;
    public float currentSpeed;

    public Istate currentState;
    public Dictionary<state, Istate> states = new Dictionary<state, Istate>();

    public int CurrentLife
    {
        get { return currentLife; } 
        set
        {
            currentLife = value;
            if (currentLife <= 0)
            {
                Transition(state.dead);
            }

        }
    }

    void Start()
    {
        states.Add(state.live, new EnemyLive(this));
        states.Add(state.dead, new EnemyDead(this));

        Transition(state.live);
    }

    void Update()
    {
        
    }

    public void OnHurt(int damage)
    {

    }

    public void Transition(state newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[newState];
        currentState.OnEnter();
    }
}
