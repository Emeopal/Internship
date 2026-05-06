using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class PlayerUp : MonoBehaviour
{
    public LineRenderer laser;
    public Player player; 
    public GameObject bullet;
    public GameObject temp;
    public GameObject soundWaveBullet;
    public Transform shootPos;
    public Transform laserShootPos;
    public Transform shootPos_1;
    public Transform shootPos_2;
    public Transform soundWaveShootPos;

    public Vector3 initRotation;
    public Vector3 initPosition;

    public PlayerDown playerDown;
    public LayerMask wall;
    public LayerMask enemy;
    public LayerMask groundLayer;

    public float shootCoolTime = 0.5f;
    public float force = 0.5f;
    public bool canShoot = true;
    public Vector3 originPos;
    public string[] targets = { "Enemy" };
    void Start()
    {
        initRotation=transform.localEulerAngles;
        initPosition = transform.localPosition;
    }

    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot )
        {
            if (originPos != null)
            {
                transform.localPosition = originPos;
            }
            StartCoroutine(coolShoot());
            RotateToMouse();
            player.currentWeapon.Shoot(transform,shootPos,bullet,player.currentReboundCount);
            player.currentWeapon.Shoot(transform, shootPos_1, shootPos_2, bullet,"Enemy",player.currentReboundCount);
            player.currentWeapon.Shoot(laser, laserShootPos, transform,wall,enemy);
            player.currentWeapon.Shoot(soundWaveShootPos, transform, soundWaveBullet);
            Offset(transform.forward);
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

    #region 
    void RotateToMouse()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        
        if (mousePosition != Vector3.zero)
        {
            Vector3 direction = mousePosition - transform.position;
            direction.y = 0; 

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }
    }
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }
    #endregion
}
