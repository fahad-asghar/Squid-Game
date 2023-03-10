using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieCompleted : MonoBehaviour
{
    public void Completed()
    {
        Game4Controller.instance.CookieCompleted();
    }
}
