using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform platformParent;
    [SerializeField] Transform finalPoint;

    public bool isNotBreakable;
    public static float time;


    public static bool enableJump;
    public static int index = 0;

    private void Awake()
    {
        time = 2;
        enableJump = false;
        index = 0;
    }

    private void Start()
    {
        ShowPath();
    }

    public void ShowPath()
    {
        if (isNotBreakable)
        {
            GetComponent<MeshRenderer>().material.DOColor(new Color32(104, 255, 100, 255), 0.4f).OnComplete(delegate ()
            {
                GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.4f).SetDelay(time).OnComplete(delegate ()
                {
                    enableJump = true;
                });
            });
        }
    }

    private void OnMouseDown()
    {
        if (enableJump)
        {
            enableJump = false;
            player.GetComponent<Animator>().SetBool("IsJumping", true);
            RotatePlayer(transform.name);

            player.transform.DOMove(transform.GetChild(0).position, 0.7f, false).SetEase(Ease.Linear).SetDelay(0.75f)
                .OnComplete(delegate ()
                {
                    player.GetComponent<Animator>().SetBool("IsJumping", false);
                    RotatePlayer("Zero");

                    if (!transform.GetComponent<JumpController>().isNotBreakable)
                    {
                        enableJump = false;
                        DestroyPlatform();
                        return;
                    }
                    transform.GetComponent<AudioSource>().Play();
                    index++;
                    EnableNextPlatforms();
                    enableJump = true;
                    Game3Controller.instance.UpdateScore();
                });
        }
    }

    private void DestroyPlatform()
    {
        player.GetComponent<Animator>().SetBool("IsHit", true);
        transform.DOScale(new Vector2(0, 0), 0.3f).SetDelay(0.2f).OnComplete(delegate ()
        {
            player.transform.DOMoveY(-6f, 1, false).SetEase(Ease.Linear).OnComplete(delegate ()
            {            
                Game3Controller.instance.GameOver();
            });
        });
    }

    private void EnableNextPlatforms()
    {
        if (index != platformParent.childCount)
        {
            platformParent.GetChild(index).GetChild(0).GetComponent<Collider>().enabled = true;
            platformParent.GetChild(index).GetChild(1).GetComponent<Collider>().enabled = true;
        }
        else
            Invoke("JumpToFinishLine", 0.5f);
           
        platformParent.GetChild(index - 1).GetChild(0).GetComponent<Collider>().enabled = false;
        platformParent.GetChild(index - 1).GetChild(1).GetComponent<Collider>().enabled = false;
    }

    private void JumpToFinishLine()
    {
        enableJump = false;
        player.GetComponent<Animator>().SetBool("IsJumping", true);

        player.transform.DOMove(finalPoint.position, 0.7f, false).SetEase(Ease.Linear).SetDelay(0.75f)
            .OnComplete(delegate ()
            {
                player.GetComponent<Animator>().SetBool("IsJumping", false);
                Game3Controller.instance.ReloadLevel();
            });
    }

    private void RotatePlayer(string direction)
    {
        if (direction.Equals("Left"))
            player.transform.DORotate(new Vector3(0, -20, 0), 0.2f, RotateMode.Fast);

        else if (direction.Equals("Right"))
            player.transform.DORotate(new Vector3(0, 20, 0), 0.2f, RotateMode.Fast);

        else if (direction.Equals("Zero"))
            player.transform.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast);
    }
}
