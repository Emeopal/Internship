using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public int scoreSum;
    public int currentLevel = 1;
}