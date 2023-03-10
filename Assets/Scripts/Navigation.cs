using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    [SerializeField] Text score1;
    [SerializeField] Text score2;
    [SerializeField] Text score3;
    [SerializeField] Text score4;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            float temp = PlayerPrefs.GetFloat("game1score");
            score1.text = "Red, Green Light: " + temp.ToString("0.00") + "m";
            score2.text = "Marbles: " + PlayerPrefs.GetInt("game2score");
            score3.text = "Glass Bridge: " + PlayerPrefs.GetInt("game3score");
            score4.text = "Dalgano Candy: " + PlayerPrefs.GetInt("game4score");
        }
    }

    public void PopUpOpen(GameObject popUp)
    {
        popUp.SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
