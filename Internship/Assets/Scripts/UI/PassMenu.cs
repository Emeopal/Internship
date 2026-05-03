using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassMenu : MonoBehaviour
{
    public GameObject[] stars;
    public GameManager gameManager;

    private void OnEnable()
    {
        for (int i = 0; i < gameManager.levels[gameManager.currentLevel - 1].Score; i++)
        {
            stars[i].SetActive(true);
        }
        if (gameManager.currentMaxLevel < gameManager.maxLevel)
        {
            gameManager.currentMaxLevel += 1;
        }
    }

    private void OnDisable()
    {
        foreach(GameObject temp in stars)
        {
            if (temp.activeSelf == true)
            {
                temp.SetActive(false);
            }
        }
    }
}
