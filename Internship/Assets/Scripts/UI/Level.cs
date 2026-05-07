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

    public List<Transform> shieldBuffPos;
    public List<Transform> speedBuffPos;
    public List<Transform> reboundBuffPos;
    public List<Transform> doubleBuffPos;
    public List<Transform> laserBuffPos;
    public List<Transform> soundWaveBuffPos;

    public GameManager gameManager;
    public GameObject passMenu;

    public GameObject normalEnemy;
    public GameObject doubleEnemy;
    public GameObject laserEnemy;
    public GameObject soundWaveEnemy;

    public GameObject shield;
    public GameObject speed;
    public GameObject rebound;
    public GameObject doubleW;
    public GameObject laserW;
    public GameObject soundWaveW;

    public Transform birthPlace;
    public Transform cameraSolidPlace;
    public Transform left;
    public Transform right;

    public float timer;
    public int Score;
    public bool isPassed;
    public Coroutine countTime;

    public GameObject Title;

    public List<GameObject> list;
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
        list = new List<GameObject>();
        foreach (Transform temp in normalPos)
        {
            _object = ObjectPool.Instance.GetObject(normalEnemy);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
            enemyCount += 1;
        }

        IEnumerator TransPos(GameObject _object,Vector3 pos)
        {
            yield return new WaitForFixedUpdate();
            _object.transform.position = pos;
            _object.transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
        

        foreach (Transform temp in doublePos)
        {
            _object = ObjectPool.Instance.GetObject(doubleEnemy);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
            enemyCount += 1;
        }

        foreach (Transform temp in laserPos)
        {
            _object = ObjectPool.Instance.GetObject(laserEnemy);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
            enemyCount += 1;
        }

        foreach (Transform temp in soundWavePos)
        {
            _object = ObjectPool.Instance.GetObject(soundWaveEnemy);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
            enemyCount += 1;
        }
        foreach (Transform temp in shieldBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(shield);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        foreach (Transform temp in speedBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(speed);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        foreach (Transform temp in reboundBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(rebound);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        foreach (Transform temp in doubleBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(doubleW);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        foreach (Transform temp in laserBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(laserW);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        foreach (Transform temp in soundWaveBuffPos)
        {
            _object = ObjectPool.Instance.GetObject(soundWaveW);
            list.Add(_object);
            StartCoroutine(TransPos(_object, temp.position));
        }
        timer = 0;
        isPassed = false;
        countTime = StartCoroutine(CountTime());
        StartCoroutine(TransPos(gameManager.player.gameObject,birthPlace.position));
        LevelOn();
    }

    public void CleanLevel()
    {
        ObjectPool.Instance.PushAllObjects();
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