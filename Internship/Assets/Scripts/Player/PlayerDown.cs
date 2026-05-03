using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{
    public Player player;

    //≥ı ºªØ”√
    public Vector3 initRotation;
    public Vector3 initPosition;
    private void Start()
    {

    }

    private void Update()
    {
        ChangeDirection();
    }
    public void ChangeDirection()
    {
        if(player.horizontalNum!=0 || player.verticalNum!=0)
            transform.LookAt(player.transform.position + player.dir);
    }

    public void Init()
    {
        transform.localEulerAngles = initRotation;
        transform.localPosition = initPosition;
    }
}
