using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public Transform target;
    public Player player;
    public Vector3 offset;

    public float percentage;
    public float speed = .003f;

    public Image HealthBar;

    Color healthyColor = Color.green;   
    Color midColor = Color.yellow;      
    Color lowColor = Color.red;

    void Update()
    {
        Follow();
        ChangeHP();
        ChangeColor();
    }

    public void Follow()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void ChangeHP()
    {
        percentage = (float)player.Life / (float)player.maxLife;
        if (HealthBar.fillAmount > percentage)
        {
            HealthBar.fillAmount -= speed;
        }
        if (HealthBar.fillAmount < percentage)
        {
            HealthBar.fillAmount =percentage;
        }

    }

    public void ChangeColor()
    {
        if (HealthBar.fillAmount < .33f)
        {
            HealthBar.color = lowColor;
            return;
        }
        if (HealthBar.fillAmount < .66)
        {
            HealthBar.color = midColor;
            return;
        }
        HealthBar.color = healthyColor;
    }

}
