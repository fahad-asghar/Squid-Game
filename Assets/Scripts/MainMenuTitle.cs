using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuTitle : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).GetComponent<Animator>().enabled = true;
        transform.DOMoveX(0, 0, false).SetDelay(0.1f)
            .OnComplete(delegate ()
            {
                transform.GetChild(1).GetComponent<Animator>().enabled = true;
                transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                    .OnComplete(delegate ()
                    {
                        transform.GetChild(2).GetComponent<Animator>().enabled = true;
                        transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                            .OnComplete(delegate ()
                            {
                                transform.GetChild(3).GetComponent<Animator>().enabled = true;
                                transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                    .OnComplete(delegate ()
                                    {
                                        transform.GetChild(4).GetComponent<Animator>().enabled = true;
                                        transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                            .OnComplete(delegate ()
                                            {
                                                transform.GetChild(5).GetComponent<Animator>().enabled = true;
                                                transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                                    .OnComplete(delegate ()
                                                    {
                                                        transform.GetChild(6).GetComponent<Animator>().enabled = true;
                                                        transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                                            .OnComplete(delegate ()
                                                            {
                                                                transform.GetChild(7).GetComponent<Animator>().enabled = true;
                                                                transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                                                    .OnComplete(delegate ()
                                                                    {
                                                                        transform.GetChild(8).GetComponent<Animator>().enabled = true;
                                                                        transform.DOMoveX(0, 0, false).SetDelay(0.1f)
                                                                            .OnComplete(delegate ()
                                                                            {

                                                                            });
                                                                    });
                                                            });
                                                    });
                                            });
                                    });
                            });
                    });
            });
    }
}
