using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : Istate
{
    public Player player;

    public PlayerLive(Player FSM)
    {
        player = FSM;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        GetNumber();
    }

    public void OnFixedUpdate()
    {
        PlayerMove();
    }

    public void OnExit()
    {

    }

    public void GetNumber()
    {
        player.horizontalNum = Input.GetAxisRaw("Horizontal");
        player.verticalNum = Input.GetAxisRaw("Vertical");
        if (player.verticalNum != 0 || player.horizontalNum != 0)
        {
            player.dir = new Vector3(player.horizontalNum, 0, player.verticalNum).normalized;
        }
        player.speedDir = new Vector3(player.horizontalNum, 0, player.verticalNum).normalized;
    }

    public void PlayerMove()
    {
        player.playerRB.velocity = player.speedDir * player.currentSpeed+ new Vector3(0,player.playerRB.velocity.y,0);
    }
    public void Move()
    {
        
    }
}
