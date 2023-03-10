using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePath : MonoBehaviour
{
    public static GeneratePath instance;

    [SerializeField] Transform platformParent;

    private void Awake()
    {
        instance = this;
        PathGenerator();

    }

    public void PathGenerator()
    {
        for (int i = 0; i < platformParent.childCount; i++)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
                platformParent.GetChild(i).GetChild(0).GetComponent<JumpController>().isNotBreakable = true;
            else if (random == 1)
                platformParent.GetChild(i).GetChild(1).GetComponent<JumpController>().isNotBreakable = true;
        }
    }
}
