using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(50, 0, 0));
            transform.localPosition = new Vector3(0, 123, -65);
        }
        else
            gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        
    }
}
