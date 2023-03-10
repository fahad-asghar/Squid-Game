using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game1Controller : MonoBehaviour
{
    [Header("GAMEPLAY COMPONENTS")]
    [SerializeField] Animator dollAnimator;
    [SerializeField] GameObject player;
    [SerializeField] GameObject rays;
    [SerializeField] Transform bullet;

    [Header("UI")]
    [SerializeField] Text scoreText;
    [SerializeField] GameObject greenLight;
    [SerializeField] GameObject redLight;
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] Text gameOverScoreText;

    [Header("EFFECTS")]
    [SerializeField] ParticleSystem bloodEffect;
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("SOUNDS")]
    [SerializeField] List<AudioClip> sounds = new List<AudioClip>();

    private float score;
    private bool checkMovement;
    private bool hitBullet;
    private bool gameOver;

    void Start()
    {
        Invoke("PlaySound", 2);
    }

    private void Update()
    {
        UpdateScore();

        if (checkMovement)
        {
            if (PlayerMovement.instance.isMoving && !hitBullet)
            {
                hitBullet = true;
                muzzleFlash.Play();
                bullet.GetComponent<AudioSource>().Play();
            }
        }

        if (hitBullet)
        {
            bullet.parent = null;
            bullet.transform.position = 
                Vector3.MoveTowards(bullet.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 0.65f, player.transform.position.z), 1f);

            if (Vector3.Distance(player.transform.position, bullet.position) < 0.7f && !gameOver)
            {
                gameOver = true;
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        CancelInvoke();
        bloodEffect.Play();
        player.GetComponent<AudioSource>().Play();
        bullet.gameObject.SetActive(false);

        Destroy(player.GetComponent<PlayerMovement>());
        player.transform.GetChild(0).GetComponent<Animator>().SetBool("IsHit", true);
        Invoke("GameOverPopUp", 2);
    }

    private void GameOverPopUp()
    {
        if (score > PlayerPrefs.GetFloat("game1score"))
            PlayerPrefs.SetFloat("game1score", score);

        gameOverScoreText.text = score.ToString("0.00") + "m";
        gameOverPopUp.SetActive(true);
    }

    private void UpdateScore()
    {
        if (gameOver)
            return;
      
        if (player.transform.rotation.eulerAngles.y > 70
            && player.transform.rotation.eulerAngles.y < 290)
            return;

        if (PlayerMovement.instance.isMoving)
        {
            score += 0.01f;
            scoreText.text = score.ToString("0.00") + "m";
        }
    }

    private void PlaySound()
    {
        dollAnimator.Play("DollAnimation2");
        greenLight.SetActive(true);
        redLight.SetActive(false);
        rays.SetActive(false);
        checkMovement = false;

        int randomClip = Random.Range(0, sounds.Count);
        GetComponent<AudioSource>().clip = sounds[randomClip];
        GetComponent<AudioSource>().Play();

        if (randomClip == 0)
            Invoke("CheckMovement", 5);
        else if (randomClip == 1)
            Invoke("CheckMovement", 3);
        else if (randomClip == 2)
            Invoke("CheckMovement", 2f);
    }

    private void CheckMovement()
    {
        dollAnimator.Play("DollAnimation1");
        redLight.SetActive(true);
        greenLight.SetActive(false);
        rays.SetActive(true);
        checkMovement = true;

        Invoke("PlaySound", Random.Range(2, 5));
    }
}
