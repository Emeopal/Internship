using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Title : MonoBehaviour
{
    public GameManager gameManager;
    public Text text;

    private void OnEnable()
    {
        text.text = $"LEVEL       {gameManager.currentLevel}";
    }
}
