using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    PhotonView PV;
    public Rigidbody rocket;

    public float fireRate = 0f;
    public float firingAngle = 45f;
    public float gravity = 50f;

    public float speed;

    private Vector3 target;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            fireRate -= Time.deltaTime;
            SetMousePositionAsTarget();
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SimulateProjectile());
            //if (fireRate <= 0)
            //{
            //    Rigidbody rb = Instantiate(rocket, transform.position, transform.rotation);
            //    rb.velocity = transform.forward * speed;
            //    fireRate = 1.5f;
            //}

        }
    }

    float ShootPower()
    {        
        float shootPower = 42f;
        float distance = Vector3.Distance(transform.position, target);
        if (true)
        {

        }

        return shootPower;
    }

    private void SetMousePositionAsTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            target = hit.point;
        float distance = Vector3.Distance(transform.position, target);
        
    }

    void CalculateShootAngleBasedOnDistance()
    {

    }

    IEnumerator SimulateProjectile()
    {
        Rigidbody rb = Instantiate(rocket, transform.position, transform.rotation);


        float target_Distance = Vector3.Distance(transform.position, target);

        
        float projectile_Velocity = target_Distance * 1.5f / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        
        float x_Velocity = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float y_Velocity = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / x_Velocity * 2;

        // Rotate projectile to face the target.
        //rb.rotation = Quaternion.LookRotation(target - rb.position);

        float elapse_time = 0;
        Vector3 tp = target;

        while (elapse_time < flightDuration)
        {
            if (!rb)
                break;
            rb.transform.Translate(0, (y_Velocity - (gravity * elapse_time)) * Time.deltaTime, x_Velocity * Time.deltaTime);
            rb.velocity = transform.forward;
            rb.rotation = Quaternion.LookRotation(tp - rb.position);
            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
