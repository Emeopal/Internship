using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class PlayerUp : MonoBehaviour
{
    public Player player; 
    public GameObject bullet;
    public GameObject temp;
    public Transform shootPos;

    public float shootCoolTime = 0.5f;
    public bool canShoot = true;
    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(coolShoot());
            RotateToMouse();
            player.currentWeapon.Shoot(transform,shootPos,bullet);
        }
    }

    IEnumerator coolShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolTime);
        canShoot = true;
    }

    #region 转向相关
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

        if (Physics.Raycast(ray, out hit))
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
