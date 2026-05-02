using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{
    public Player player;


    private void Start()
    {


    }

    private void Update()
    {
        ChangeDirection();
    }
    public void ChangeDirection()
    {
        transform.LookAt(player.transform.position + player.dir);
    }
}
