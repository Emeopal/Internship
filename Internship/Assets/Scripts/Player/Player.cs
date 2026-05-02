using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public enum state
{
    live,dead
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

    [Header("组件")]
    public Rigidbody playerRB;
    public BoxCollider playerColl;

    [Header("子类组件")]
    public PlayerDown playerDown;
    public PlayerUp playerUp;

    [Header("子类物体")]
    public GameObject normalWeapon;
    public GameObject doubleWeapon;

    public Vector3 dir;
    public Vector3 speedDir;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            if (life <= 0)
            {
                Transition(state.dead);
            }
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

        GameWeapons.Add(weapon.normalWeapon, normalWeapon);
        GameWeapons.Add(weapon.doubleWeapon, doubleWeapon);

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

        currentReboundCount = initReboundCount;
        buffs[buff.shield].StopBuff();
        buffs[buff.speed].StopBuff();
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
            currentGameWeapon.SetActive(false);
        currentWeapon = weapons[newWeapon];
        currentGameWeapon = GameWeapons[newWeapon];
        currentGameWeapon.SetActive(true);
    }
}
