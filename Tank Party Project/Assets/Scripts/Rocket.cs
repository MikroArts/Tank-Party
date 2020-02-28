using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform target;
    public float speed = 45f;

    

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
       
    }

    

    private void HitTarget()
    {
        Destroy(gameObject);
    }
}
