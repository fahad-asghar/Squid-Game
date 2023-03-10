using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Ball"))
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.name = "Detected";
            Game2Controller.instance.UpdateScore();
        }
    }
}
