using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float AxisX;
    float AxisY;

    public float distance = 50;
    public float speed = 3;

    private void Start()
    {
        SetCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            SetCameraPosition();
    }

    void SetCameraPosition()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        AxisX += deltaY * speed;
        AxisY += deltaX * speed;

        //if (AxisX > 0)
        //    AxisX = 0;
        //if (AxisX < -89)
        //    AxisX = -89;     does the same as Mathf.Clamp(); below

        AxisX = Mathf.Clamp(AxisX, -89, 0);

        var rotation = Quaternion.Euler(AxisX, AxisY, 0);
        transform.position = rotation * Vector3.forward * distance;

        transform.LookAt(Vector3.up * 5);
    }
}
