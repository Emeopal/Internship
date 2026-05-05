using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("视觉设置")]
    public float viewRadius = 10f;
    public float viewAngle = 60f;
    public Transform upperBody;  // 敌人上半身的 Transform
    public Transform eyePosition;  // 眼睛位置（可选）

    [Header("检测设置")]
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    [Header("调试")]
    public bool showGizmos = true;

    private void Start()
    {
        if (eyePosition == null)
            eyePosition = transform;
    }

    public bool IsInSight(Transform target)
    {
        if (target == null || upperBody == null)
            return false;

        Transform visionSource = eyePosition != null ? eyePosition : transform;
        Vector3 toTarget = target.position - visionSource.position;
        float distance = toTarget.magnitude;

        if (distance > viewRadius)
            return false;

        // 使用上半身的方向作为视野方向
        Vector3 viewDirection = upperBody.forward;
        float angle = Vector3.Angle(viewDirection, toTarget);

        if (angle > viewAngle / 2f)
            return false;

        if (Physics.Raycast(visionSource.position, toTarget.normalized, out RaycastHit hit, distance, obstacleLayer))
        {
            if (hit.collider.gameObject != target.gameObject)
                return false;
        }

        return true;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        if (upperBody == null)
            return;

        Transform visionSource = eyePosition != null ? eyePosition : transform;
        Vector3 forward = upperBody.forward;  // 使用上半身方向

        // 绘制视野范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(visionSource.position, viewRadius);

        // 绘制视野边界线
        Vector3 leftDir = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 rightDir = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(visionSource.position, leftDir * viewRadius);
        Gizmos.DrawRay(visionSource.position, rightDir * viewRadius);

        // 绘制扇形区域
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        DrawFanArc(visionSource.position, forward, viewRadius, viewAngle);
    }

    void DrawFanArc(Vector3 center, Vector3 forward, float radius, float angle)
    {
        int segments = 20;
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float currentAngle = Mathf.Lerp(-angle / 2f, angle / 2f, t);
            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Gizmos.DrawRay(center, dir * radius);
        }
    }
}