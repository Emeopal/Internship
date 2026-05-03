using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public int currentLevel;
    public int currentMaxLevel;
    public int maxLevel;

    public void Awake()
    {
        maxLevel = levels.Count;
    }
}