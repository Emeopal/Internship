using UnityEngine;
using System.Collections.Generic;

public class SoundWaveProject : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;
    public int damage = 3;
    public float maxRadius = 8f;
    public float fanAngle = 60f;
    public int meshSegments = 20;
    public float visualScale = 3f;
    public float startAlpha = 1f;
    public float endAlpha = 0.2f;

    private Vector3 direction;
    private float startTime;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    private Material soundWaveMat;

    private void Awake()
    {
        if (sphereCollider == null)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.1f;
        }
    }
    public void Initialize(Vector3 position, Vector3 dir)
    {
        transform.position = position;
        direction = dir.normalized;
        startTime = Time.time;

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            soundWaveMat = new Material(Shader.Find("Unlit/Transparent"));
            meshRenderer.material = soundWaveMat;
        }
        
        Mesh initialMesh = CreateFanMesh(0.1f);
        meshFilter.mesh = initialMesh;
        
    }

    void Update()
    {
        Vector3 moveDir = direction;
        moveDir.y = 0;
        moveDir.Normalize();

        transform.position += moveDir * speed * Time.deltaTime;

        float elapsed = Time.time - startTime;
        float currentRadius = Mathf.Lerp(0.1f, maxRadius, elapsed / lifetime);

        if (meshFilter != null)
        {
            Mesh currentMesh = CreateFanMesh(currentRadius);
            meshFilter.mesh = currentMesh;
        }
        
        if (sphereCollider != null)
        {
            sphereCollider.radius = currentRadius;
        }

        if (elapsed >= lifetime)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    Mesh CreateFanMesh(float radius)
    {
        Mesh mesh = new Mesh();
        mesh.name = "FanMesh";

        int vertexCount = meshSegments + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        Color[] colors = new Color[vertexCount];
        int[] triangles = new int[meshSegments * 3];

        float halfAngle = fanAngle / 2f;
        float startAngle = -halfAngle;

        vertices[0] = Vector3.zero;
        colors[0] = new Color(1, 1, 1, startAlpha);

        for (int i = 0; i <= meshSegments; i++)
        {
            float angle = Mathf.Lerp(startAngle, halfAngle, (float)i / meshSegments);
            float rad = angle * Mathf.Deg2Rad;
            float visualRadius = radius * visualScale;
            vertices[i + 1] = new Vector3(Mathf.Sin(rad) * visualRadius, 0.5f, Mathf.Cos(rad) * visualRadius);

            float alpha = Mathf.Lerp(startAlpha, endAlpha, (float)i / meshSegments);
            colors[i + 1] = new Color(1, 1, 1, alpha);
        }

        for (int i = 0; i < meshSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    bool IsValidTarget(GameObject obj)
    {
        if (obj == null) return false;
        if (obj.CompareTag("Player")) return false;

        if (obj.CompareTag("Enemy"))
        {
            return true;
        }
        
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject))
        {
            Vector3 toEnemy = (other.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, toEnemy);

            if (angle <= fanAngle / 2f)
            {
                FSM enemyFSM;
                if (other.TryGetComponent<FSM>(out enemyFSM))
                    enemyFSM.OnHurt(damage);
            }
        }
    }
}