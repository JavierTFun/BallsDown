using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float smoothSpeed = 0.125f;
    public float maxFallingSpeed = 10f;
    public bool falling = false;

    void Start()
    {
        transform.position = new Vector3(0, -5, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void LateUpdate()
    {
        if (target.position.y < 38 && target.GetComponent<Rigidbody>().velocity.y < 0)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        if (falling)
        {
            Vector3 desiredPosition = target.position + Vector3.up * height;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
        else
        {
            transform.position = new Vector3(770, 150, 0);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}

