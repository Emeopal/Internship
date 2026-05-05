using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour
{
    public Player player;

    public Vector3 initRotation;
    public Vector3 initPosition;

    public void Awake()
    {
        initPosition = transform.localEulerAngles;
        initPosition = transform.localPosition;
    }
    private void Start()
    {

    }

    private void Update()
    {
        ChangeDirection();
    }
    public void ChangeDirection()
    {
        if (Mathf.Abs(player.horizontalNum) > 0.1f || Mathf.Abs(player.verticalNum) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void Init()
    {
        transform.localEulerAngles = initRotation;
        transform.localPosition = initPosition;
    }
}