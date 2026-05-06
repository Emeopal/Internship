using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    live,dead,
    patrol,chase,
};

public enum buff {
    shield,speed
};

public class Player : MonoBehaviour
{
    [Header("初始数值")]
    public int maxLife = 3;
    public float initSpeed = 5;
    public int initReboundCount = 3;

    [Header("当前数值")]
    public int life;
    public float currentSpeed;
    public int currentReboundCount;

    [Header("状态")]
    public bool canBeHurt = true;

    public float horizontalNum;
    public float verticalNum;
    public float coolHurtTime = .5f;

    [Header("组件")]
    public Rigidbody playerRB;
    public BoxCollider playerColl;

    [Header("子类组件")]
    public PlayerDown playerDown;
    public PlayerUp playerUp;
    public Transform safePlace;
    public HP m_hp;

    [Header("子类物体")]
    public GameObject normalWeapon;
    public GameObject doubleWeapon;
    public GameObject laserWeapon;
    public GameObject soundWaveWeapon;

    public Vector3 dir;
    public Vector3 speedDir;
    public Vector3 initRotation;

    public Coroutine coolHurtTimer;
    public GameManager gameManager;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (value <= 0)
            {
                life = 0;
                Transition(state.dead);
            }
            life = value;
        }

    }

    public Istate currentState;
    public state m_state;
    public Weapon currentWeapon;
    public GameObject currentGameWeapon;

    public Dictionary<weapon, Weapon> weapons = new Dictionary<weapon, Weapon>();
    public Dictionary<weapon, GameObject> GameWeapons = new Dictionary<weapon, GameObject>();
    public Dictionary<state, Istate> states = new Dictionary<state, Istate>();
    public Dictionary<buff, Ibuff> buffs = new Dictionary<buff, Ibuff>();

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerColl = GetComponent<BoxCollider>();

        states.Add(state.live, new PlayerLive(this));
        states.Add(state.dead, new PlayerDead(this));

        buffs.Add(buff.speed, new speed(this));
        buffs.Add(buff.shield, new shield(this));

        weapons.Add(weapon.normalWeapon, new NormalWeapon());
        weapons.Add(weapon.doubleWeapon, new DoubleWeapon());
        weapons.Add(weapon.laserWeapon, new LaserWeapon());
        weapons.Add(weapon.soundWaveWeapon, new SoundWaveWeapon());

        GameWeapons.Add(weapon.normalWeapon, normalWeapon);
        GameWeapons.Add(weapon.doubleWeapon, doubleWeapon);
        GameWeapons.Add(weapon.laserWeapon, laserWeapon);
        GameWeapons.Add(weapon.soundWaveWeapon, soundWaveWeapon);

        initRotation = transform.localEulerAngles;

        Init();
    }

    public void Update()
    {
        currentState.OnUpdate();
    }

    public void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }
    public void Init()
    {
        Life = maxLife;
        currentSpeed = initSpeed;
        canBeHurt = true;

        Transition(state.live);
        TransWeapon(weapon.normalWeapon);

        transform.localEulerAngles = initRotation;
        transform.position = gameManager.levels[gameManager.currentLevel - 1].birthPlace.position;

        playerUp.enabled = true;
        playerDown.enabled = true;
        playerUp.Init();
        playerDown.Init();

        currentReboundCount = initReboundCount;
        buffs[buff.shield].StopBuff();
        buffs[buff.speed].StopBuff();
        m_hp.ShowHealthBar();
    }

    public void ToSafe()
    {
        transform.position = safePlace.position;
        this.enabled = false;
    }

    public void OnHurt(int damage)
    {
        if (canBeHurt)
        {
            Life -= damage;
            canBeHurt = false;
            m_hp.ShowHealthBar();
            coolHurtTimer = StartCoroutine(CoolHurt());
        }
    }

    IEnumerator CoolHurt()
    {
        yield return new WaitForSeconds(coolHurtTime);
        canBeHurt = true;
    }


    public void Transition(state newState)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[newState];
        m_state = newState;
        currentState.OnEnter();
    }

    public void TransWeapon(weapon newWeapon)
    {
        if (currentGameWeapon != null)
        {
            currentGameWeapon.SetActive(false);
            GameWeapons[newWeapon].transform.rotation = currentGameWeapon.transform.rotation;
        }
        currentWeapon = weapons[newWeapon];
        currentGameWeapon = GameWeapons[newWeapon];
        currentGameWeapon.SetActive(true);
        playerUp = currentGameWeapon.GetComponent<PlayerUp>();
    }
}
