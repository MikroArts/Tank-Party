using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, turnSpeed;
    float h, v;
    private Rigidbody player;
    void Start()
    {
        player = GetComponent<Rigidbody>();        
    }
   void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h * moveSpeed, 0, v * moveSpeed);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), turnSpeed * Time.deltaTime);
        }

    }
        
}
