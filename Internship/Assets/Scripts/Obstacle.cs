using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 originPos;
    public float speed;
    public bool startReturn = false;
    public Coroutine down;
    public LayerMask player;
    public LayerMask enemy;

    private void Start()
    {
        originPos = transform.position;
        offset = new Vector3(transform.position.x, -1, transform.position.z);
        speed = .9f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (down!=null)
            {
                StopCoroutine(down);
            }
            down = StartCoroutine(Down());
        }
    }

    private void Update()
    {
        if (startReturn)
        {
            Check();
        }
    }

    void Check()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 3,player)|| 
            Physics.Raycast(transform.position, Vector3.up, out hit, 3, player))
        {
            return;
        }
        startReturn = false;
        StartCoroutine(Up());
    }

    IEnumerator Down()
    {
        while (transform.position.y-offset.y>.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, offset, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        transform.position = offset;
        yield return new WaitForSeconds(1.5f);
        startReturn = true;
    }

    IEnumerator Up()
    {
        while (transform.position.y - originPos.y < -.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        transform.position = originPos;
    }
}
