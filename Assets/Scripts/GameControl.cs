using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AD.Services;
using System.Collections.Generic;
using System;

public class GameControl : MonoBehaviour
{
    System.DateTime timeOutTimeStart;
    static public GameControl instance { get { return i_Instance; } }
    static protected GameControl i_Instance;
    private Move mm;
    //[HideInInspector]
    public static BalloonFly balloonFly;
    public static SpawnerObject spawObj;
    [Header("UI")]
    //public Canvas CanvasGame;
    public GameObject heart1, heart2, heart3;
    public GameObject[] lives;
    public Text scoreTextGame;
    public Text scoreTextEnd;
    public Text highScoreText;
    public GameObject GameOverCanvas;
    public GameObject PlayCanvas;
    public GameObject PauseCanvas;
    public GameObject Game;
    public Button Retray_Button;
    public Button Resume_Button;
    public Button Pause_Button;
    public Button Play_Button;
    public RectTransform PauseMenu;
    [SerializeField]
    private int score = 0;
    private int death=3;
    [SerializeField]
    private int highScore;
    public float scrollSpeed = -1.5f;
    public bool gameOver = false;
    public bool start = false;
    public bool pause = false;
    public bool resume = false;
    public bool retry = false;
    [Header("Prefabs")]
    public GameObject Balloon;
    private GameObject bal;
    public GameObject[] block;
    int lv = 3;
    void Awake()
    {
        foreach (var item in block)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(true);
        }
        start = false;
        i_Instance = this;
        PauseCanvas.SetActive(false);
        //Destroy(gameObject);
        LogService.SendMissionStarted(new System.Collections.Generic.Dictionary<string, object>());
        Retray_Button.onClick.AddListener(LoadScene);
        Resume_Button.onClick.AddListener(Resume_);
        Play_Button.onClick.AddListener(Play_Canvas);
        PauseMenu.gameObject.SetActive(false);
        Pause_Button.onClick.AddListener(GamePause);
    }
    private void Update()
    {
        //if (balloonFly.isDead == true)
        //{
        //    balloonFly.GetComponent<Rigidbody2D>().gravityScale = 0;
            
        //}
    }
    public void Player_Scored()
    {
        if (gameOver)
            return;
        score++;
        scoreTextGame.text = "Score: " + score.ToString();
        scoreTextEnd.text = "Score: " + score.ToString();
        if (score > highScore)
        {
            highScoreText.text = "High score: " + score.ToString();
            PlayerPrefs.SetFloat("High Score: ", score);
        }
        else
        {
            highScoreText.text = highScore.ToString();
        }

    }
    public void GamePause()
    {
        pause = true;
        PauseCanvas.SetActive(true);
        bal.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        Time.timeScale = 0;
        if (score > highScore)
        {
            highScoreText.text = "High score: " + score.ToString();
            PlayerPrefs.SetFloat("High Score: ", score);
        }
        else
        {
            highScoreText.text = highScore.ToString();
        }

    }
    public void BalloonDied(string reason)
    {
        GameOverCanvas.SetActive(true);
        gameOver = true;
        Debug.Log("gameover true");
        var eventData = new Dictionary<string, object>();
        eventData.Add("deathReason", reason);
        LogService.SendMissionFailed(eventData);
       
        if (gameOver== true && death==3)
        {
            heart1.SetActive(false);
            death--;
            Debug.Log(death);   
        }
        else if (death == 2)
        {
            heart2.SetActive(false);
            death--;
        }
    }
    public void Play_Canvas()
    {
        foreach (var item in block)
        {
            item.SetActive(true);
        }
        start = true;
        gameOver = false;
        PlayCanvas.SetActive(false);
        Game.SetActive(true);
        Balloon.SetActive(true);
        bal = Instantiate(Balloon, new Vector2(-19, 10), Quaternion.identity);
        bal.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
        Debug.Log(start);
    }
  
    public void LoadScene()
    {
        foreach (var item in block)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
        start = true;
        Debug.Log("loadscene");
        GameOverCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
    }
    public void Resume_()
    {
        resume = true;
        pause = false;
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        Debug.Log(pause+"    "+ Time.timeScale);
        bal.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        bal.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}
