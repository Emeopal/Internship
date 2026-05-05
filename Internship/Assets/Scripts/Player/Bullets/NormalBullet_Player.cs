using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet_Player : MonoBehaviour
{
    public Coroutine m_Pos;

    public Rigidbody bulletRB;
    public Transform startPos;

    public int reboundCount = 3;
    public int damage = 1;

    private Vector3 currentVelocity;
    public Vector3 prepareAngel;

    public Vector3 originPos;
    public Vector3 originRot;
    public Vector3 originVel;

    public FSM fsm;


    public int ReboundCount
    {
        get { return reboundCount; }
        set
        {
            reboundCount = value;
            if (reboundCount <= 0)
            {
                ObjectPool.Instance.PushObject(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (bulletRB != null && bulletRB.useGravity == false)
        {
            bulletRB.velocity = currentVelocity;
        }
    }
    private void OnEnable()
    {
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        m_Pos = StartCoroutine(InitPos());
    }

    IEnumerator InitPos()
    {
        yield return new WaitForFixedUpdate();
        transform.localEulerAngles = new Vector3(90, prepareAngel.y, prepareAngel.z);
        transform.position = startPos.position;
    }
    private void Start()
    {
        originVel = bulletRB.velocity;
        originRot = transform.eulerAngles;
        originPos = transform.position;
    }

    public void Init()
    {
        transform.position = originPos;
        transform.eulerAngles = originRot;
        bulletRB.velocity = originVel;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FSM fsm;
            fsm = other.GetComponent<FSM>();
            fsm.OnHurt(damage);
            ObjectPool.Instance.PushObject(gameObject);
            return;
        }
        if (other.CompareTag("Wall"))
        {
            Rebound(other);
        }

    }
    void Rebound(Collider wall)
    {
        if (currentVelocity.magnitude < 0.1f)
            return;

        Vector3 bulletDir = currentVelocity.normalized;
        Vector3 wallNormal = wall.transform.up;

        if (Mathf.Abs(wallNormal.y) > 0.9f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position - bulletDir * 0.5f, bulletDir, out hit, 1f))
            {
                wallNormal = hit.normal;
            }
        }

        wallNormal.y = 0;
        if (wallNormal == Vector3.zero)
        {
            wallNormal = -bulletDir;
        }

        Vector3 reflectDir = Vector3.Reflect(bulletDir, wallNormal);
        reflectDir.y = 0;

        currentVelocity = reflectDir * currentVelocity.magnitude;
        transform.rotation = Quaternion.LookRotation(currentVelocity);
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        ReboundCount--;
    }
    public void SetVelocity(Vector3 velocity)
    {
        currentVelocity = velocity;
        if (bulletRB != null)
        {
            bulletRB.velocity = currentVelocity;
        }
    }

    private void OnDisable()
    {
        if(m_Pos!=null)
            StopCoroutine(m_Pos);
        Init();
    }
}