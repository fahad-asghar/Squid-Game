using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game3Controller : MonoBehaviour
{
    public static Game3Controller instance;

    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverScoreText;
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] Image fader;

    [SerializeField] Transform platformParent;
    [SerializeField] Transform player;

    private int score;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("game3score") < score)
            PlayerPrefs.SetInt("game3score", score);

        gameOverScoreText.text = score.ToString();
        gameOverPopUp.SetActive(true);
    }

    public void ReloadLevel()
    {
        fader.DOFade(0, 0).OnComplete(delegate ()
        {
            fader.gameObject.SetActive(true);
            fader.DOFade(1, 0.5f).OnComplete(delegate ()
            {
                JumpController.enableJump = false;
                JumpController.index = 0;
                player.transform.position = new Vector3(0, 0, -0.8f);

                for (int i = 0; i < platformParent.childCount; i++)
                {
                    platformParent.GetChild(i).GetChild(0).GetComponent<JumpController>().isNotBreakable = false;
                    platformParent.GetChild(i).GetChild(1).GetComponent<JumpController>().isNotBreakable = false;

                    platformParent.GetChild(i).GetChild(0).GetComponent<Collider>().enabled = false;
                    platformParent.GetChild(i).GetChild(1).GetComponent<Collider>().enabled = false;
                }
                platformParent.GetChild(0).GetChild(0).GetComponent<Collider>().enabled = true;
                platformParent.GetChild(0).GetChild(1).GetComponent<Collider>().enabled = true;

                
                fader.DOFade(0, 0.5f).OnComplete(delegate ()
                {
                    fader.gameObject.SetActive(false);

                    if(JumpController.time > 0.2f)
                        JumpController.time -= 0.2f;

                    GeneratePath.instance.PathGenerator();

                    for (int i = 0; i < platformParent.childCount; i++)
                    {
                        platformParent.GetChild(i).GetChild(0).GetComponent<JumpController>().ShowPath();
                        platformParent.GetChild(i).GetChild(1).GetComponent<JumpController>().ShowPath();
                    }

                });
                
            });
        });


    
    }


}
