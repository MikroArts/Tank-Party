using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRocketHolder : MonoBehaviour
{
    private Vector3 target;
    PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
                target = hit.point;
            Vector3 y = new Vector3(0, .002f, 0);
            transform.LookAt(target);
        }        
    }
}
