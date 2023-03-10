using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2Controller : MonoBehaviour
{
    public static Game2Controller instance;

    [SerializeField] Text timerText;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverScoreText;
    [SerializeField] GameObject gameOverPopUp;

    [SerializeField] int timer;
    [HideInInspector] public bool gameOver = false;
    private int score;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating("StartTimer", 1, 1);
    }

    private void StartTimer()
    {
        if (timer > 0)
        {
            timer -= 1;
            timerText.text = timer.ToString();
        }
        else
        {
            gameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        if (score > PlayerPrefs.GetInt("game2score"))
            PlayerPrefs.SetInt("game2score", score);

        gameOverScoreText.text = score.ToString();
        gameOverPopUp.SetActive(true);
    }

    public void UpdateScore()
    {
        if (!gameOver)
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
  

 
  
}
