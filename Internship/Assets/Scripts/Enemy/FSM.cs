using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [Header("初始数值")]
    public int maxLife;
    public float initSpeed;

    [Header("当前数值")]
    public int currentLife;
    public float currentSpeed;

    [Header("视野")]
    public EnemyVision vision;

    [Header("巡逻边界")]
    public Transform bottomLeft;
    public Transform topRight;

    [Header("子类")]
    public EnemyUp enemyUp;
    public EnemyDown enemyDown;
    public HP_Enemy m_hp;

    public Transform player;
    public Rigidbody rb;
    public GameObject corpse;

    public Istate currentState;
    public Dictionary<state, Istate> states = new Dictionary<state, Istate>();
    public weapon m_weapon;
    public Weapon currentWeapon;
    public Dictionary<weapon, Weapon> weapons = new Dictionary<weapon, Weapon>();

    public Vector3 targetPosition;
    private Vector3 originAng;
    private bool hasTarget = false;

    public int CurrentLife
    {
        get { return currentLife; } 
        set
        {
            if (value <= 0)
            {
                currentLife = 0;
                Transition(state.dead);
            }
            currentLife = value;
        }
    }

    private void Awake()
    {
        weapons.Add(weapon.normalWeapon, new NormalWeapon());
        weapons.Add(weapon.doubleWeapon, new DoubleWeapon());
        weapons.Add(weapon.laserWeapon, new LaserWeapon());
        weapons.Add(weapon.soundWaveWeapon, new SoundWaveWeapon());

        states.Add(state.patrol, new EnemyPatrol(this));
        states.Add(state.chase, new EnemyChase(this));
        states.Add(state.dead, new EnemyDead(this));
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

        originAng = transform.localEulerAngles;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Init();
        
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        targetPosition = new Vector3();
        currentSpeed = initSpeed;
        currentLife = maxLife;
        transform.localEulerAngles = originAng;
        enemyUp.Init();
        enemyDown.Init();
        currentWeapon = weapons[m_weapon];
        Transition(state.patrol);
        m_hp.ShowHealthBar();
    }

    void Update()
    {
        currentState.OnUpdate();
    }

    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    public void OnHurt(int damage)
    {
        CurrentLife -= damage;
        m_hp.ShowHealthBar();
    }

    public void Transition(state newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[newState];
        currentState.OnEnter();
    }

    #region 巡逻
    public Vector3 GetRandomPointInBounds()
    {
        if (bottomLeft == null || topRight == null)
        {
            return transform.position;
        }

        float minX = bottomLeft.localPosition.x;
        float maxX = topRight.localPosition.x;
        float minZ = bottomLeft.localPosition.z;
        float maxZ = topRight.localPosition.z;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    public void SetNewPatrolTarget()
    {
        Vector3 newTarget = GetRandomPointInBounds();
        // 确保目标点和当前位置有一定距离，避免原地踏步
        if (Vector3.Distance(newTarget, transform.localPosition) < 1f)
        {
            // 如果距离太近，重新生成目标点
            newTarget = GetRandomPointInBounds();
        }
        targetPosition = newTarget;
        hasTarget = true;
    }

    public bool MoveToTarget(float speed)
    {
        if (!hasTarget)
            return false;

        if (bottomLeft == null || topRight == null)
            return false;

        Vector3 direction = (targetPosition - transform.localPosition);
        direction.y = 0;
        float distance = direction.magnitude;

        if (distance < 0.5f)
        {
            hasTarget = false;
            return true;
        }

        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;

        if (enemyDown != null && direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyDown.transform.rotation = Quaternion.Slerp(enemyDown.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        return false;
    }

    public bool HasReachedTarget()
    {
        return !hasTarget;
    }
    #endregion

    private void OnDisable()
    {
        Init();
    }
}
