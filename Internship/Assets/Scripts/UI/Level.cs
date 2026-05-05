using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public List<Transform> normalPos = new List<Transform>();
    public List<Transform> doublePos = new List<Transform>();
    public List<Transform> laserPos = new List<Transform>();
    public List<Transform> soundWavePos = new List<Transform>();

    public GameManager gameManager;
    public GameObject passMenu;

    public GameObject normalEnemy;
    public GameObject doubleEnemy;
    public GameObject laserEnemy;
    public GameObject soundWaveEnemy;

    public Transform birthPlace;
    public Transform cameraSolidPlace;
    public Transform upper;
    public Transform lower;
    public Transform left;
    public Transform right;

    public float timer;
    public int Score;
    public bool isPassed;
    public Coroutine countTime;

    public GameObject Title;

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
                isPassed = true;
                StartCoroutine(Pass());
            }
        }
    }
    public IEnumerator Pass()
    {
        EndLevel();
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
        GameObject _object;
        foreach(Transform temp in normalPos)
        {
            _object=ObjectPool.Instance.GetObject(normalEnemy);
            _object.transform.position = temp.position;
            enemyCount += 1;
        }

        foreach (Transform temp in doublePos)
        {
            _object = ObjectPool.Instance.GetObject(doubleEnemy);
            _object.transform.position = temp.position;
            enemyCount += 1;
        }

        foreach (Transform temp in normalPos)
        {
            _object = ObjectPool.Instance.GetObject(laserEnemy);
            _object.transform.position = temp.position;
            enemyCount += 1;
        }

        foreach (Transform temp in normalPos)
        {
            _object = ObjectPool.Instance.GetObject(soundWaveEnemy);
            _object.transform.position = temp.position;
            enemyCount += 1;
        }
        timer = 0;
        isPassed = false;
        countTime = StartCoroutine(CountTime());
        LevelOn();
    }

    public void LevelOn()
    {
        Title.SetActive(true);
        StartCoroutine(WaitSomeTime());
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(1.5f);
        Title.SetActive(false);
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void EndLevel()
    {
        if (countTime != null)
        {
            StopCoroutine(countTime);
            if (timer <= 30)
            {
                Score = 3;
                return;
            }
            if (timer <= 60)
            {
                Score = 2;
                return;
            }
            Score = 1;
        }
    }
}