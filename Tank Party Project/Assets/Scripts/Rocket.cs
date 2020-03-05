using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocket;

    void OnEnable()
    {
        rocket = GetComponent<Rigidbody>();
    }

    void Update()
    {        
        //transform.rotation = Quaternion.LookRotation(rocket.velocity);
        //RotateRocketAlongTrajectory();  
    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }

    void RotateRocketAlongTrajectory()
    {
        float x_Velocity = rocket.velocity.x;
        float y_Velocity = rocket.velocity.y;
        float z_Velocity = rocket.velocity.z;

        float squareXZ = Mathf.Sqrt(x_Velocity * x_Velocity + z_Velocity * z_Velocity);
        

        float rocketRotation = -1f * Mathf.Atan2(y_Velocity, squareXZ) * 180 / Mathf.PI;

        transform.eulerAngles = new Vector3(rocketRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
