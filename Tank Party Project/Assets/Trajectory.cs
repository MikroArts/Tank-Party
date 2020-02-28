using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    LineRenderer trajectory;

    Vector3 startPoint;
    Vector3 targetPoint;

    public float velocity, angle;
    float radiant;
    float gravity;
    public int resolution = 10;
    float distance;

    void Start()
    {
        

        trajectory = GetComponent<LineRenderer>();        
        gravity = Mathf.Abs(Physics.gravity.y);        
    }

   
    void Update()
    {        
        startPoint = transform.position;
        DrawLineFromOriginToTarget();

        transform.LookAt(targetPoint);
        

        RenderTrajectory();
    }

    private void RenderTrajectory()
    {
        trajectory.positionCount = resolution + 1;
        trajectory.SetPositions(CalculateTrajectory());
        trajectory.useWorldSpace = true;
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];
        
        radiant = ConvertFloatToRadiant(angle);

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            float y = distance * Mathf.Tan(radiant) / 4;

            Vector3 h = new Vector3(0, y, 0);
            lineArray[i] = SetVerticePosition(t,startPoint,targetPoint, h);
        }

        return lineArray;
    }

    private Vector3 SetVerticePosition(float t, Vector3 startPoint, Vector3 targetPoint, Vector3 height)
    {
        float oneMinusT = 1 - t;
        float squareOneMinusT = oneMinusT * oneMinusT;

        Vector3 verticePosition = squareOneMinusT * startPoint + 2 * oneMinusT * t * (startPoint + height + height) + t * t * targetPoint;

        return verticePosition;
    }

    private float ConvertFloatToRadiant(float angle)
    {
        print(Mathf.PI + " | " + Mathf.PI / 180);
        return angle * Mathf.PI / 180;
    }

    private void DrawLineFromOriginToTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPoint = hit.point;
        }
        distance = Vector3.Distance(startPoint, targetPoint);
        //Debug.DrawLine(transform.position, targetPoint, Color.blue);
    }

}
