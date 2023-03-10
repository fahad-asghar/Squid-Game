using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveRays : MonoBehaviour
{
    void Start()
    {
        MoveRay();
    }

    private void MoveRay()
    {
        transform.DOLocalRotate(new Vector3(-1.56f, Random.Range(-13.0f, 1.0f), 0), 5, RotateMode.Fast)
            .SetSpeedBased().OnComplete(delegate ()
            {
                MoveRay();
            });         
    }
}
