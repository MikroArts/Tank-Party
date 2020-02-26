using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public List<Transform> players;
    private Camera camera;
    public Vector3 offset;
    private Vector3 velocity;

    public float smoothTime = .5f;
    public float minZoom = 27f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    float greatestDistance;

    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (players.Count == 0)
            return;
        Move();
        Zoom();
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, greatestDistance / zoomLimiter);
        
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        Vector3 middlePoint = CalculateMiddlePoint();

        Vector3 newPosition = middlePoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private Vector3 CalculateMiddlePoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        greatestDistance = bounds.size.z;
        return bounds.center;
    }
}
