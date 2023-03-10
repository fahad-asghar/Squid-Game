using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        if (player != null)
            transform.position = 
                new Vector3(transform.position.x, transform.position.y, player.position.z + 20f);
    }
}
