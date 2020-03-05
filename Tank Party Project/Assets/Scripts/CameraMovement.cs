using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(45, 0, 0));
        transform.localPosition = new Vector3(0, 150, -100);        
    }
}
