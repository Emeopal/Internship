using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    public Vector3 initRotation;
    public Vector3 initPosition;

    public void Init()
    {
        transform.localEulerAngles = initRotation;
        transform.localPosition = initPosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Awake()
    {
        initPosition = transform.localEulerAngles;
        initPosition = transform.localPosition;
    }
}
