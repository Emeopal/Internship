using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBackGround : MonoBehaviour
{
    public Image HealthBar;

    private void Update()
    {
        transform.position = HealthBar.transform.position;
    }
}
