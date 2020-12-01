using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameControllAirplane : MonoBehaviour
{
    System.DateTime timeOutTimeStart;
    static public GameControllAirplane instance { get { return i_Instance; } }
    static protected GameControllAirplane i_Instance;
    //[HideInInspector]
    public  SpawnerObject spawObj;
    [Header("UI")]
    public GameObject heart1, heart2, heart3;
    public GameObject[] lives;
    public Text scoreTextGame;
    public Text scoreTextEnd;
    public Text highScoreText;
    public Text pauseScoreText;
    public GameObject GameOverCanvas;
    public GameObject PlayCanvas;
    public GameObject PauseCanvas;
    public GameObject Game;
    public Button Retray_Button;
    public Button Resume_Button;
    public Button Pause_Button;
    public Button Play_Button;
    public AxisTouchButton Fly_up;
    public AxisTouchButton Fly_Down;
    public RectTransform PauseMenu;
    [SerializeField]
    private int score = 0;
    private int death = 3;
    [SerializeField]
    private int highScore;
    public float scrollSpeed = -1.5f;
    public bool gameOver = false;
    public bool start = false;
    public bool pause = false;
    public bool resume = false;
    public bool retry = false;
    public bool spawnObj = false;
    [Header("Prefabs")]
    public GameObject Plane;
    private GameObject plane;
    public GameObject[] block;
    public bool scroling=false;
    AxisTouchButton ATB;
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
        i_Instance = this;
        //LogService.SendMissionStarted(new System.Collections.Generic.Dictionary<string, object>());
        Retray_Button.onClick.AddListener(Retray);
        Resume_Button.onClick.AddListener(Resume_);
        Play_Button.onClick.AddListener(Play_Canvas);
        Pause_Button.onClick.AddListener(GamePause);
    }
    public void DragonScored()
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
        Time.timeScale = 0;
        
            pauseScoreText.text = "Score: " + score.ToString();
            PlayerPrefs.SetFloat("High Score: ", score);
       
    }
    public void BalloonDied(string reason)
    {
        GameOverCanvas.SetActive(true);
        gameOver = true;
        start = false;
        plane.SetActive(false);
        Debug.Log("gameover true");
        var eventData = new Dictionary<string, object>();
        eventData.Add("deathReason", reason);
        scroling = false;
        spawnObj = false;
        //LogService.SendMissionFailed(eventData);

        if (gameOver == true)
        {
            Destroy(plane);
            heart1.SetActive(false);
        }
        else if (death == 2)
        {
            heart2.SetActive(false);
            death--;
            Debug.Log(death);
        }
    }
    public void Play_Canvas()
    {
        foreach (var item in block)
        {
            item.SetActive(true);
        }
        spawnObj = true;
      
        start = true;
        gameOver = false;
        scroling = true;
        Time.timeScale = 1;
        PlayCanvas.SetActive(false);
        Game.SetActive(true);
        plane = Instantiate(Plane, new Vector2(-5, 0), Quaternion.identity);
        Debug.Log(start);
        Fly_up.enabled = true; Fly_Down.enabled = true;
    }
    public void Retray()
    {
        retry = true;
        GameOverCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        Fly_up.enabled = false; Fly_Down.enabled = false;
    }

    public void Resume_()
    {
        resume = true;
        pause = false;
        Time.timeScale = 1;
        PauseCanvas.SetActive(false); 
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitUntil(()=>start == true);
    }
}
