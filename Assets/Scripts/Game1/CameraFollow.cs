using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public float cameraSpeed = 0.1f;

    void Start()
    {
        transform.position = player.position + cameraOffset;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 finalPosition = player.position + cameraOffset;
            Vector3 lerpPosition = Vector3.Lerp(transform.position, finalPosition, cameraSpeed);
            transform.position = lerpPosition;
        }
    }
}
