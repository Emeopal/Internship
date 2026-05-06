using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet_Enemy : MonoBehaviour
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
        bulletRB.velocity = Vector3.zero;
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        m_Pos = StartCoroutine(InitPos());
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
    IEnumerator InitPos()
    {
        yield return new WaitForFixedUpdate();
        transform.localEulerAngles = new Vector3(90, prepareAngel.y, prepareAngel.z);
        transform.position = startPos.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Rebound(collision);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player;
            if (other.gameObject.TryGetComponent<Player>(out player))
                player.OnHurt(damage);
            ObjectPool.Instance.PushObject(gameObject);
            return;
        }
    }

    void Rebound(Collision collision)
    {
        if (currentVelocity.magnitude < 0.1f)
            return;

        Vector3 wallNormal = collision.contacts[0].normal;
        wallNormal.y = 0;
        wallNormal.Normalize();

        Vector3 bulletDir = currentVelocity.normalized;

        Vector3 reflectDir = Vector3.Reflect(bulletDir, wallNormal);
        reflectDir.y = 0;
        reflectDir.Normalize();

        currentVelocity = reflectDir * currentVelocity.magnitude;

        if (currentVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(currentVelocity);
            transform.localEulerAngles = transform.localEulerAngles =
                new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

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
        if (m_Pos != null)
            StopCoroutine(m_Pos);
        Init();
    }
}