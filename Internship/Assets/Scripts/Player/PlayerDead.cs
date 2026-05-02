using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : Istate
{
    public Player player;
    public PlayerDead(Player FSM)
    {
        player = FSM;
    }
    public void OnEnter()
    {
        player.playerDown.enabled = false;
        player.playerUp.enabled = false;
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
        player.playerDown.enabled = true;
        player.playerUp.enabled = true;
    }
}
