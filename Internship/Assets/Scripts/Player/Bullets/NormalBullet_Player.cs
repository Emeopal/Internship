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
    public Player player;

    public string enemy;

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
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.position = startPos.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemy))
        {
            FSM enemyFSM;
            if (other.TryGetComponent<FSM>(out enemyFSM))
                enemyFSM.OnHurt(damage);
            Player player;
            if (other.TryGetComponent<Player>(out player))
                player.OnHurt(damage);
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(m_Pos);
    }
}
