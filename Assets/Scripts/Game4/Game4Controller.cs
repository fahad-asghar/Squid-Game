using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Game4Controller : MonoBehaviour
{
    public static Game4Controller instance;

    public List<GameObject> cookies = new List<GameObject>();

    [SerializeField] GameObject circle;
    [SerializeField] GameObject star;
    [SerializeField] GameObject triangle;
    [SerializeField] GameObject umbrella;

    [SerializeField] Image bar;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverScoreText;
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] Image fader;

    [SerializeField] Transform camera;

    private bool enableClick = false;
    private bool clicked = false;
    private GameObject cookie;

    private int time;
    private int score;

    public float barIncrementValue;
    public float barDecrementValue;

    private bool enableCount;
    private float count;

    [SerializeField] List<Transform> points = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadList();
        GenerateCookiesInRandomOrder();
        MoveCamera();
        SetBar();
        SetTimer();
        SetCount();
    }

    private void SetCount()
    {
        enableCount = false;
        count = 0;
    }

    private void SetTimer()
    {
        time = 20;
        timeText.text = time.ToString();
    }

    private void StartTimer()
    {
        if (time > 0)
        {
            time -= 1;
            timeText.text = time.ToString();
            Invoke("StartTimer", 1);
        }
        else
            GameOver();
    }

    private void GameOver()
    {
        CancelInvoke();
        enableClick = false;
        cookie.GetComponent<Animator>().SetFloat("Speed", 0);

        if (PlayerPrefs.GetInt("game4score") < score)
            PlayerPrefs.SetInt("game4score", score);

        gameOverScoreText.text = score.ToString();
        gameOverPopUp.SetActive(true);
    }

    private void SetBar()
    {
        bar.DOColor(Color.white, 0);
        bar.transform.parent.gameObject.SetActive(false);
        bar.GetComponent<Image>().fillAmount = 0;
    }

    private void LoadList()
    {
        cookies.Add(circle);
        cookies.Add(star);
        cookies.Add(triangle);
        cookies.Add(umbrella);
    }

    private void UpdateScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    private void GenerateCookiesInRandomOrder()
    {
        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, cookies.Count);
            GameObject obj = Instantiate(cookies[random], points[i].position, Quaternion.Euler(-90, 180, 0), points[i]);
           
            cookies.Remove(cookies[random]);          
        }
    }

    private void MoveCamera()
    {
        camera.transform.DOMove(new Vector3(-0.06f, 2.537963f, -19.129f), 2, false);
        camera.transform.DORotate(new Vector3(46.704f, 1.13f, 0.384f), 2f, RotateMode.Fast);
        camera.GetComponent<Camera>().DOFieldOfView(77, 2).OnComplete(delegate() {
            EnableCookieColliders();
        });
    }

    private void EnableCookieColliders()
    {
        for (int i = 0; i < points.Count; i++)
            points[i].GetComponent<Collider>().enabled = true;
    }

    private void DisableCookieColliders()
    {
        for (int i = 0; i < points.Count; i++)
            points[i].GetComponent<Collider>().enabled = false;
    }

    public void CookieClick(Transform cookie)
    {
        DisableCookieColliders();
        this.cookie = cookie.transform.GetChild(0).gameObject;

        if (cookie.name.Equals("Point1"))
        {
            camera.transform.DOMove(new Vector3(-0.625f, 1.874952f, -17.0917f), 2, false);
            camera.transform.DORotate(new Vector3(85, 0, 0), 2f, RotateMode.Fast);
            camera.GetComponent<Camera>().DOFieldOfView(40, 2).OnComplete(delegate () {
            });
        }

        else if (cookie.name.Equals("Point2"))
        {
            camera.transform.DOMove(new Vector3(-0.212f, 1.874952f, -17.0917f), 2, false);
            camera.transform.DORotate(new Vector3(85, 0, 0), 2f, RotateMode.Fast);
            camera.GetComponent<Camera>().DOFieldOfView(40, 2).OnComplete(delegate () {
            });
        }

        else if (cookie.name.Equals("Point3"))
        {
            camera.transform.DOMove(new Vector3(0.234f, 1.874952f, -17.0917f), 2, false);
            camera.transform.DORotate(new Vector3(85, 0, 0), 2f, RotateMode.Fast);
            camera.GetComponent<Camera>().DOFieldOfView(40, 2).OnComplete(delegate () {
            });
        }

        else if (cookie.name.Equals("Point4"))
        {
            camera.transform.DOMove(new Vector3(0.642f, 1.874952f, -17.0917f), 2, false);
            camera.transform.DORotate(new Vector3(85, 0, 0), 2f, RotateMode.Fast);
            camera.GetComponent<Camera>().DOFieldOfView(40, 2).OnComplete(delegate () {
            });
        }

        cookie.GetChild(0).GetChild(3).transform.DOLocalMoveZ(0.03f, 1, false).OnComplete(delegate ()
        {
            cookie.GetChild(0).GetChild(3).transform.DOLocalMoveX(0.15f, 1, false).OnComplete(delegate ()
            {
                cookie.GetChild(0).GetChild(3).gameObject.SetActive(false);
                cookie.GetChild(0).GetChild(2).gameObject.SetActive(true);
                enableClick = true;
                bar.transform.parent.gameObject.SetActive(true);
                Invoke("StartTimer", 1);
            });
        });
    }

    public void CookieCompleted()
    {
        GetComponent<AudioSource>().Play();
        UpdateScore();
        SetBar();
        CancelInvoke();
        enableClick = false;
        cookie.GetComponent<Animator>().SetFloat("Speed", 0);

        cookie.transform.GetChild(0).transform.DOLocalMove(new Vector3(0.00011f, 0.008f, 0.0188f), 2, false);
        cookie.transform.GetChild(0).transform.DOLocalRotate(new Vector3(70, 0, 0), 2, RotateMode.LocalAxisAdd);

        camera.DOMove(new Vector3(camera.position.x, 1.36f, -17.58f), 2, false);
        camera.DORotate(new Vector3(26.6f, 0, 0), 2, RotateMode.Fast).OnComplete(delegate() {
            Invoke("RestartGame", 2);

        });
    }

    private void RestartGame()
    {
        fader.DOFade(0, 0).OnComplete(delegate ()
        {
            fader.gameObject.SetActive(true);
            fader.DOFade(1, 1).OnComplete(delegate ()
            {
                camera.DOMove(new Vector3(0, 2.013224f, -27.73f), 0, false);
                camera.DORotate(new Vector3(16.193f, 0, 0), 0, RotateMode.Fast);

                for (int i = 0; i < points.Count; i++)
                    Destroy(points[i].GetChild(0).gameObject);

                if(barIncrementValue > 0.002f)
                    barIncrementValue = barIncrementValue - 0.002f;

                if(barDecrementValue < 0.04f)
                    barDecrementValue = barDecrementValue + 0.002f;

                LoadList();
                GenerateCookiesInRandomOrder();
                SetBar();
                SetTimer();
                SetCount();

                fader.DOFade(0, 1).OnComplete(delegate (){
                    fader.gameObject.SetActive(false);
                    MoveCamera();
                });
            });
        });
    }

    void Update()
    {
        if (enableClick)
        {
            if (Input.GetMouseButton(0))
            {
                clicked = true;
                if (cookie.GetComponent<Animator>().GetFloat("Speed") < 0.5f)
                    cookie.GetComponent<Animator>().SetFloat("Speed", cookie.GetComponent<Animator>().GetFloat("Speed") + barIncrementValue);
                else
                    enableCount = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                bar.DOColor(Color.white, 1);
                SetCount();
                clicked = false;
            }

            if (enableCount)
            {
                bar.DOColor(Color.red, 1);

                count += 0.1f;
                if (count >= 3)
                    GameOver();
            }

            if (!clicked)
            {
                if (cookie.GetComponent<Animator>().GetFloat("Speed") >= 0.0f)               
                    cookie.GetComponent<Animator>().SetFloat("Speed", cookie.GetComponent<Animator>().GetFloat("Speed") - barDecrementValue);             
            }

            bar.GetComponent<Image>().fillAmount = cookie.GetComponent<Animator>().GetFloat("Speed") * 2;
        }
    }
}
