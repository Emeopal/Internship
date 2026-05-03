using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    public GameManager gameManager;
    public Button[] buttons;

    public void Awake()
    {
        gameManager = Camera.main.GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        for(int i = 0; i < gameManager.currentMaxLevel; i++)
        {
            buttons[i].interactable = true;
        }
        for(int i = gameManager.currentMaxLevel; i < gameManager.maxLevel; i++)
        {
            buttons[i].interactable = false;
        }
    }
}
