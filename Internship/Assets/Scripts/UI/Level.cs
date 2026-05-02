using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Transform> normalPos = new List<Transform>();
    public List<Transform> doublePos = new List<Transform>();
    public List<Transform> laserPos = new List<Transform>();
    public List<Transform> soundPos = new List<Transform>();

    public GameManager gameManager;
    public GameObject passMenu;

    public GameObject normalEnemy;
    public GameObject doubleEnemy;
    public GameObject laserEnemy;
    public GameObject soundEnemy;

    public Transform birthPlace;
    public Transform cameraSolidPlace;
    public Transform upper;
    public Transform lower;
    public Transform left;
    public Transform right;

    private int enemyCount = 0;
    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
        set
        {
            enemyCount = value;
            if (enemyCount <= 0)
            {
                StartCoroutine(Pass());
            }
        }
    }
    public IEnumerator Pass()
    {
        yield return new WaitForSeconds(5);
        passMenu.SetActive(true);
    }


    private void Awake()
    {
        gameManager = Camera.main.GetComponent<GameManager>();
    }

    public void Init()
    {
        enemyCount = 0;
        //这里写生成地生成敌人
    }
}