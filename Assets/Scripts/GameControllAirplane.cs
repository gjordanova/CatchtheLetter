using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameControllAirplane : MonoBehaviour
{
    System.DateTime timeOutTimeStart;
    static public GameControllAirplane instance { get { return i_Instance; } }
    static protected GameControllAirplane i_Instance;
    public Slider LevelSlider;
    //[HideInInspector]
    public  SpawnerObject spawObj;
    [Header("UI")]
   
    //public GameObject[] lives;
    [Header("Texts")]
    public Text scoreTextGame;
    public Text scoreTextEnd;
    public Text highScoreText;
    public Text pauseScoreText;
    public Text Level;

    [Header("Buttons")]
    public Button RetrayGame_Button;
    public Button Resume_Button;
    public Button Pause_Button;
    public Button Play_Button;
    public Button TryAgainLevel_Button;
    public Button Continue_Button;
    public AxisTouchButton Fly_up;
    public AxisTouchButton Fly_Down;
    public RectTransform PauseMenu;

    [SerializeField]
    [Header("Ints")]
    private int score;
    private int max_Score=30;
    internal int death = 3;
    public int level;
    int level_counter = 0;
    private int highScore;
    private int timeEndLevel = 20;
    internal int secunde;
    int lv = 3;

    [SerializeField]
    [Header("Floats")]
    public float scrollSpeed = -1.5f;
    public float timeLevel;

    [Header("Bools")]
    public bool gameOver = false;
    public bool start = false;
    public bool pause = false;
    public bool resume = false;
    public bool retryGame = false;
    public bool tryAgainLevel = false;
    public bool spawnObj = false;
    public bool continue_game = false;
    public bool scroling = false;
    public bool youWin = false;

    [Header("Prefabs")]
    public GameObject heart1, heart2, heart3;
    public GameObject Plane;
    protected GameObject plane;
    public GameObject[] block;
    public GameObject GameOverCanvas;
    public GameObject PlayCanvas;
    public GameObject PauseCanvas;
    public GameObject Game;
    public GameObject YouWin;
    public GameObject Try_Again;
    public Text CatchTheLetter;
    AxisTouchButton ATB;
    public ParticleSystem part;
    public GameObject ranlett;
    TMP_Text Letter;
    string Lett;
    int counter;
    string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    void Awake()
    {
        Lett = Alphabet[Random.Range(0, Alphabet.Length)].ToString();
        Letter = ranlett.GetComponentInChildren<TMP_Text>();
        Letter.text = Lett.ToString();
        CatchTheLetter.text = "Catch The Letter: " + Letter.text;
        foreach (var item in block)
        {
            item.SetActive(false);
        }
        i_Instance = this;
        //LogService.SendMissionStarted(new System.Collections.Generic.Dictionary<string, object>());
        RetrayGame_Button.onClick.AddListener(RetrayGame);
        Resume_Button.onClick.AddListener(Resume_);
        Play_Button.onClick.AddListener(Play_Canvas);
        Pause_Button.onClick.AddListener(GamePause);
        level = 1;
        score = 0;
        Level.text = "Level: " + level.ToString();
        scoreTextGame.text = "Score: " + score.ToString();
       
    }
    private void Update()
    {

        if (start == true)
        {
            if (Input.GetMouseButton(0))
            {

                counter++;
                if (counter > 2)
                {
                    CatchTheLetter.enabled = false;
                }

            }
            timeLevel += Time.deltaTime;
            secunde = (int)timeLevel % 60;
            //LevelSlider.minValue = secunde;
            LevelSlider.value = Mathf.PingPong(secunde, timeEndLevel);


        }
        
        //Debug.Log(secunde);
        LevelSlider.maxValue = timeEndLevel;
        if (secunde == timeEndLevel)
        {
            youWin = true;
            Player_Win();
            timeLevel = 0.0f;
            CatchTheLetter.enabled = true;
            counter = 0;
            
        }
    }
    public void AirplaneScored()
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
        else if (score == max_Score)
        {
            Debug.Log("you win");
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
    public void AirplaneDied(string reason)
    {
        start = false;
        Time.timeScale = 0;
        //plane.SetActive(false);
        timeLevel = 0;
        Debug.Log("gameover true");
        var eventData = new Dictionary<string, object>();
        eventData.Add("deathReason", reason);
        tryAgainLevel = true;
        scroling = false;
        spawnObj = false;
        continue_game = false;
        Fly_up.enabled = false; Fly_Down.enabled = false;
        //LogService.SendMissionFailed(eventData);

        if (death == 1)
        {
            GameOverCanvas.SetActive(true);
            gameOver = true;
            death--; 
            Debug.Log("death 0" + death);
            heart1.SetActive(false);
            RetrayGame_Button.onClick.AddListener(RetrayGame);
            //retry = true;
        }
        else if (tryAgainLevel == true && death == 2)
        {
            heart2.SetActive(false);
            death--;
            Try_Again.SetActive(true);
            TryAgainLevel_Button.onClick.AddListener(RePlayLevel);
            Debug.Log("death 1" + death);
        }
        else if (tryAgainLevel == true && death == 3)
        {
            heart3.SetActive(false);
            death--;
            Try_Again.SetActive(true);
            TryAgainLevel_Button.onClick.AddListener(RePlayLevel);
            Debug.Log("death 2"+death);
        }
        //TryAgain_Button.onClick.AddListener(Retray);
    }
    public void RePlayLevel()
    {
        foreach (var item in block)
        {
            item.SetActive(true);
        }
        Time.timeScale = 1;
        //gameOver = false;
        
        spawnObj = true;
        start = true;
        scroling = true;
        tryAgainLevel = false;
        Try_Again.SetActive(false);
        Fly_up.enabled = true; Fly_Down.enabled = true;
        Game.SetActive(true);
        Plane.GetComponent<Collider2D>().isTrigger = false;
      
    }
    public void Play_Canvas()
    {
        foreach (var item in block)
        {
            item.SetActive(true);
        }

        tryAgainLevel = false;
        spawnObj = true;
        start = true;
        gameOver = false;
        scroling = true;
        Time.timeScale = 1;
        PlayCanvas.SetActive(false);
        Game.SetActive(true);
        plane = Instantiate(Plane, new Vector2(-5, 0), Quaternion.identity);
        Fly_up.enabled = true; Fly_Down.enabled = true;
        retryGame = false;
        Letter_R(spawObj.spawner_enemy.Object.GetComponent<RandomLetter>().Letter.text, level, level_counter);
    }
    public void Player_Win()
    {
        YouWin.SetActive(true);
        Fly_up.enabled = false; Fly_Down.enabled = false;
        scroling = true;
        //continue_game = true;
        Time.timeScale = 0;
        Game.SetActive(true);
        part.GetComponent<ParticleSystem>().enableEmission=true;
        part.enableEmission=true;
        Letter_R(Letter.text, level, level_counter);
        Continue_Button.onClick.AddListener(Continue_Game);
       
        Debug.Log(level);
    }
    public void Continue_Game()
    {
        YouWin.SetActive(false);
        continue_game = true;
        level++;
        level_counter++;
        Level.text = "Level: " + level.ToString();
        foreach (var item in block)
        {
            item.SetActive(true);
        }
        Time.timeScale = 1;
        spawnObj = true;
        start = true;
        Fly_up.enabled = true; 
        Fly_Down.enabled = true;
        youWin = false;
        
    }
    public void RetrayGame()
    {
        retryGame = true;
        Time.timeScale = 1;
        tryAgainLevel = false;
        GameOverCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        Fly_up.enabled = false; Fly_Down.enabled = false;
        Destroy(plane);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        level = 1;
        score = 0;
        death = 3;
        timeLevel = 0.0f;
        Debug.Log("level "+level + "score "+score + "smrt"+death);
        Score(level, score);
        SceneManager.LoadScene("Air_Palne");
    }
    public void Resume_()
    {
        resume = true;
        pause = false;
        Time.timeScale = 1;
        PauseCanvas.SetActive(false); 
    }
    public void Letter_R(string s, int _level, int _level_counter)
    {
        if (spawObj.spawner_enemy.Object.GetComponent<RandomLetter>().Letter.text != Letter.text)
        {
            Debug.Log("razlicni se");
        }
        else
        {

        }


        if (_level > _level_counter)
        {
            Lett = Alphabet[Random.Range(0, Alphabet.Length)].ToString();
            Letter = ranlett.GetComponentInChildren<TMP_Text>();
            Letter.text = Lett.ToString();
            CatchTheLetter.text = "Catch The Letter: " + Letter.text;
            //Debug.Log(spawObj.spawner_enemy.Object.GetComponent<RandomLetter>().Letter.text);
          
        }
        else
        {

        }
        
      
    }
    public void LevelCalculate(int _level,int _score, int _death, int _timeEndLevel)
    {
        
    }
    public void Score(int _level,int _score)
    {
        if (gameOver==true && _level==1)
        {
            scoreTextGame.text = "Score: " + _score.ToString();
            Level.text = "Level: " + _level.ToString();
        }
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitUntil(()=>start == true);
    }
}
//class temp: GameControllAirplane { 

//    public virtual void semple()
//    {
//        foreach (var item in block)
//        {
//            item.SetActive(true);
//        }
//        spawnObj = true;
//        start = true;
//        gameOver = false;
//        scroling = true;
//        Time.timeScale = 1;
//        Game.SetActive(true);
//        plane = Instantiate(Plane, new Vector2(-5, 0), Quaternion.identity);
//        Debug.Log(start);
//        Fly_up.enabled = true; Fly_Down.enabled = true;
//    }


//}
//class Win : temp
//{
//    public override void semple()
//    {
//        gameOver = true;
//        YouWin.SetActive(true);
//    }
//}
