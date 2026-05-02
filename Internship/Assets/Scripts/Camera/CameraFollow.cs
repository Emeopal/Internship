using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform targetPos;
    public Vector3 targetPosition;
    public Vector3 offset;
    public Vector3 desiredPos;
    public Vector3 smoothPos;

    public float speed = 3;
    public Toggle follow;

    private void FixedUpdate()
    {
        if (!follow.isOn)
            return;
        targetPosition = targetPos.position + offset;
        if (transform.position == targetPosition)
        {
            return;
        }
        smoothPos = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = smoothPos;
    }



}