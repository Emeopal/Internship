using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : Istate
{
    public Player player;
    public GameManager gameManager;
    public Coroutine playerFail;
    public PlayerDead(Player FSM)
    {
        player = FSM;
        gameManager = Camera.main.GetComponent<GameManager>();
    }
    public void OnEnter()
    {
        player.playerDown.enabled = false;
        player.playerUp.enabled = false;
        Time.timeScale = 0;
        if (gameManager.levels[gameManager.currentLevel-1].isPassed!=true)
        {
            playerFail = player.StartCoroutine(WaitFail());
        }
    }
    IEnumerator WaitFail()
    {
        yield return new WaitForSeconds(1);
        Camera.main.GetComponent<UI>().GameFail();
    }

    public void OnUpdate()
    {
        if (gameManager.levels[gameManager.currentLevel - 1].EnemyCount == 0)
        {
            player.StopCoroutine(playerFail);
        }
    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
        player.playerDown.enabled = true;
        player.playerUp.enabled = true;
        Time.timeScale = 1;
    }
}
