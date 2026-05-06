using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyUp : MonoBehaviour
{
    public Vector3 initRotation;
    public Vector3 initPosition;

    public LineRenderer laser;
    public FSM fsm;
    public GameObject bullet;
    public GameObject temp;
    public GameObject soundWaveBullet;
    public Transform shootPos;
    public Transform laserShootPos;
    public Transform shootPos_1;
    public Transform shootPos_2;
    public Transform soundWaveShootPos;

    public LayerMask wall;
    public LayerMask player;
    public string[] targets = { "Player" };

    public float shootCoolTime = 3f;
    public float force = 0.5f;
    public Vector3 originPos;

    public bool canShoot = true;

    public void Awake()
    {
        initRotation = transform.localEulerAngles;  // 修正：原来是 initPosition
        initPosition = transform.localPosition;
    }

    void Start()
    {

    }

    public void Shoot()
    {
        if (canShoot)
        {
            // 添加：射击前强制朝向玩家
            FaceTarget();

            if (originPos != null)
            {
                transform.localPosition = originPos;
            }
            StartCoroutine(coolShoot());
            if (shootPos)
            {
                fsm.currentWeapon.Shoot(transform, shootPos, bullet, 3);
            }
            else if (shootPos_1 && shootPos_2)
            {
                fsm.currentWeapon.Shoot(transform, shootPos_1, shootPos_2, bullet, "Player", 3);
            }
            else if (laserShootPos)
            {
                fsm.currentWeapon.Shoot(laser, laserShootPos, transform, wall, player);
            }
            else if (soundWaveShootPos)
            {
                fsm.currentWeapon.Shoot(soundWaveShootPos, transform, soundWaveBullet);
            }
            Offset(transform.forward);
        }
    }

    // 新增：让 enemyUp 朝向目标（玩家）
    void FaceTarget()
    {
        if (fsm != null && fsm.player != null)
        {
            Vector3 toTarget = fsm.player.position - transform.position;
            toTarget.y = 0;  // 保持水平方向

            if (toTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(toTarget);
                transform.rotation = targetRotation;
            }
        }
    }

    IEnumerator coolShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolTime);
        canShoot = true;
    }

    public void Offset(Vector3 dir)
    {
        originPos = transform.localPosition;
        transform.localPosition -= dir * force;
        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        while (Vector3.Distance(transform.localPosition, originPos) > .1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originPos, force * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void Init()
    {
        transform.localEulerAngles = initRotation;
        transform.localPosition = initPosition;
    }

    private void OnDisable()
    {
        canShoot = true;
    }
}
