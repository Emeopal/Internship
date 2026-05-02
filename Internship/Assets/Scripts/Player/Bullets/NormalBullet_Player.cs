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
        
    }
    private void OnEnable()
    {
        m_Pos=StartCoroutine(InitPos());
    }

    IEnumerator InitPos()
    {
        yield return new WaitForFixedUpdate();
        transform.position = startPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FSM>(out fsm))
        {
            fsm.OnHurt(damage);
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(m_Pos);
    }
}
