using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        Game4Controller.instance.CookieClick(transform);
    }
}
