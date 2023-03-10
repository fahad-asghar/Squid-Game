using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeBall : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    Vector2 startPos;
    Vector2 endPos; 
    Vector2 direcion;
    float touchTimeStart;
    float touchTimeEnd;
    float timeIntervl;
    float throwForceInXandY = 0.2f;
    float throwForceInZ = 100f;

    void Update()
    {
        if (!Game2Controller.instance.gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchTimeStart = Time.time;
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<AudioSource>().Play();
                touchTimeEnd = Time.time;
                timeIntervl = touchTimeEnd - touchTimeStart;
                endPos = Input.mousePosition;
                direcion = endPos - startPos;

                GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
                obj.name = "Ball";
                obj.GetComponent<Rigidbody>().isKinematic = false;

                if (direcion != Vector2.zero)
                {
                    obj.GetComponent<Rigidbody>().AddForce(direcion.x * throwForceInXandY, direcion.y * throwForceInXandY, throwForceInZ / timeIntervl);
                    Destroy(obj, 2);
                }
            }
        }
    }
}
