using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelGenerator : MonoBehaviour
{
    [SerializeField] Transform sceneSection1;
    [SerializeField] Transform sceneSection2;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Trigger1"))
            sceneSection2.position = new Vector3(0, 0, sceneSection1.position.z + 50);

        else if (collision.CompareTag("Trigger2"))
            sceneSection1.position = new Vector3(0, 0, sceneSection2.position.z + 50);
    }
}
