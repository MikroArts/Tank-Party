using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    private Rigidbody player;
    public Rigidbody rocket;
    public Transform spawnPoint;
    public Transform rocketHolder;

    public float fireRate = 0f;
    public float firingAngle = 45f;
    public float gravity = 50f;

    public float moveSpeed, turnSpeed;
    float h, v;
    
    Vector3 target;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        player = GetComponent<Rigidbody>();
    }
   void FixedUpdate()
    {        
        if (PV.IsMine)
        {
            
            fireRate -= Time.deltaTime;            
            Move();

            RotateRocketHolder();
            if (Input.GetMouseButtonDown(0))
            {
                SetMousePositionAsTarget();
                PV.RPC("Shoot", RpcTarget.All);
            }            
        }        
        
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position -= transform.right * moveSpeed * Time.deltaTime;
            transform.Rotate(-transform.up * turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += transform.right * moveSpeed * Time.deltaTime;
            transform.Rotate(transform.up * turnSpeed * Time.deltaTime);
        }
    }
    private void RotateRocketHolder()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            target = hit.point;
        Vector3 y = new Vector3(0, .002f, 0);
        rocketHolder.LookAt(target);
    }    
    [PunRPC]
    private void Shoot()
    {
        if (fireRate <= 0)
        {
            StartCoroutine(SimulateProjectile());
            fireRate = .7f;
        }
        
    }
    private void SetMousePositionAsTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            target = hit.point;
        float distance = Vector3.Distance(transform.position, target);

    }
    [PunRPC]
    IEnumerator SimulateProjectile()
    {
        GameObject rocketPrefab = PhotonNetwork.Instantiate(Path.Combine("Prefab", "Rocket"), spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = rocketPrefab.GetComponent<Rigidbody>();


        float target_Distance = Vector3.Distance(spawnPoint.position, target);       

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
            rb.velocity = spawnPoint.forward;
            rb.rotation = Quaternion.LookRotation(tp - rb.position);
            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
