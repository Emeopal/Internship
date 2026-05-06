using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Istate
{
    public FSM fsm;
    private float patrolWaitTime = 2f;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    public float upperBodyRotateSpeed = 30;  // 增加旋转速度

    public EnemyPatrol(FSM enemyState)
    {
        fsm = enemyState;
    }

    public void OnEnter()
    {
        isWaiting = false;
        waitTimer = 0f;
        fsm.SetNewPatrolTarget();
        Debug.Log(fsm.targetPosition);
    }

    public void OnUpdate()
    {
        if (fsm.player != null && fsm.vision != null)
        {
            bool canSeePlayer = fsm.vision.IsInSight(fsm.player);

            if (canSeePlayer)
            {
                FacePlayer();
                fsm.Transition(state.chase);
                return;
            }
        }

        // 巡逻时：上半身随机旋转搜索，下半身朝巡逻目标移动
        if (!isWaiting && fsm.enemyUp != null)
        {
            fsm.enemyUp.transform.Rotate(0, upperBodyRotateSpeed * Time.deltaTime, 0);
        }

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                isWaiting = false;
                fsm.SetNewPatrolTarget();
            }
            return;
        }

        bool reached = fsm.MoveToTarget(fsm.currentSpeed);

        if (reached)
        {
            isWaiting = true;
            waitTimer = patrolWaitTime;
        }
    }
    
    void FacePlayer()
    {
        if (fsm.enemyUp != null && fsm.player != null)
        {
            Vector3 toPlayer = fsm.player.position - fsm.enemyUp.transform.position;
            toPlayer.y = 0;
            if (toPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
                fsm.enemyUp.transform.rotation = targetRotation;
            }
        }
    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
    }

}

public class EnemyChase : Istate
{
    public FSM fsm;

    [Header("追逐设置")]
    public float chaseSpeed = 5f;
    public float loseSightTime = 2f;
    public float stopChaseDistance = 15f;

    private float loseSightTimer = 0f;
    private bool hasLostSight = false;

    public EnemyUp enemyUp;

    public EnemyChase(FSM enemyState)
    {
        fsm = enemyState;
        enemyUp = enemyState.enemyUp;
    }

    public void OnEnter()
    {
        loseSightTimer = 0f;
        hasLostSight = false;
    }

    public void OnUpdate()
    {
        if (fsm.player == null)
        {
            ReturnToPatrol();
            return;
        }

        // 持续朝向玩家（关键修改）
        FacePlayer();

        if (fsm.vision != null)
        {
            bool canSeePlayer = fsm.vision.IsInSight(fsm.player);

            if (canSeePlayer)
            {
                loseSightTimer = 0f;
                hasLostSight = false;
                ChasePlayer();
            }
            else
            {
                HandleLoseSight();
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    void FacePlayer()
    {
        if (enemyUp != null && fsm.player != null)
        {
            Vector3 toPlayer = fsm.player.position - enemyUp.transform.position;
            toPlayer.y = 0;
            if (toPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
                // 平滑旋转
                enemyUp.transform.rotation = Quaternion.Slerp(
                    enemyUp.transform.rotation,
                    targetRotation,
                    Time.deltaTime * 8f
                );
            }
        }
    }
    void ChasePlayer()
    {
        if (fsm.player == null)
            return;

        Vector3 targetPos = fsm.player.position;
        Vector3 direction = (targetPos - fsm.transform.position);
        direction.y = 0;
        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            direction.Normalize();
            fsm.transform.position += direction * chaseSpeed * Time.deltaTime;
        }

        // 下半身朝向移动方向
        if (fsm.enemyDown != null && direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            fsm.enemyDown.transform.rotation = Quaternion.Slerp(
                fsm.enemyDown.transform.rotation,
                targetRotation,
                Time.deltaTime * 5f
            );
        }

        enemyUp.Shoot();
    }

    void HandleLoseSight()
    {
        loseSightTimer += Time.deltaTime;

        if (loseSightTimer >= loseSightTime)
        {
            hasLostSight = true;
        }

        if (hasLostSight)
        {
            float distanceToPlayer = Vector3.Distance(
                fsm.transform.position,
                fsm.player.position
            );

            if (distanceToPlayer > stopChaseDistance)
            {
                ReturnToPatrol();
            }
        }
    }

    void ReturnToPatrol()
    {
        fsm.Transition(state.patrol);
    }
}