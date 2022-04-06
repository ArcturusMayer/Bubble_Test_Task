using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenerationHandler : MonoBehaviour
{
    // Balls prefabs
    public GameObject redBall;
    public GameObject yellowBall;
    public GameObject greenBall;
    public GameObject blueBall;

    // Static object, parent for top ball row, moves to move all balls down
    public GameObject topRowHandler;
    protected GameObject ball;

    // Generation instance, provides algorithm for color choosing
    private Generation generation;

    // Constants for level generating
    protected const float levelPaddingY = 0.8f;
    protected const float levelPaddingX = 0.9f;
    protected const float ballRadius = 0.4f;
    protected Vector2 screenSize;

    // Start positions for spawning balls
    protected float leftBigRowMaxX;
    protected float leftSmallRowMaxX;

    // Width of level in balls, depends on screen size
    protected int ballsInBigRow = 0;
    // Difficulty, how much rows. Always even
    protected int rowsAmount = 6;

    // Speed of slow balls shift
    protected float speed = 0.1f;

    // Win condition
    private GameObject toWin;
    // amount of generations before winning
    public int waves = 0;

    // Variables for timer
    private float waveTime = 53.0f;
    private float minWaveTime = 35.0f;
    private float waveTimeReduce = 3.0f;
    private float previousTime = 0.0f;
    private float timer = 0.0f;

    //UI
    public Text timerDisplay;
    public Text wavesDisplay;
    public Button restart;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        generation = LevelChooser.getGeneration();
        clearVariables();
        init();
        generateBalls();
        restart.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        shift();
        if (Time.timeSinceLevelLoad - previousTime < waveTime)
        {
            if (toWin == null)
            {
                init();
                toWin = generateBalls();
                waves++;
                wavesDisplay.text = "Waves passed: " + waves.ToString();
                previousTime = Time.timeSinceLevelLoad;
                if(waveTime > minWaveTime)
                {
                    waveTime -= waveTimeReduce;
                }
            }
        }
        else
        {
            if (waves > PlayerPrefs.GetInt(LevelChooser.getPrefName()))
            {
                PlayerPrefs.SetInt(LevelChooser.getPrefName(), waves);
            }
            clearVariables();
            SceneManager.LoadScene("Menu");
        }
        if(Time.timeSinceLevelLoad - timer >= 1.0f)
        {
            timerDisplay.text = "Time left: " + Mathf.RoundToInt(waveTime - (Time.timeSinceLevelLoad - previousTime)).ToString();
            timer = Time.timeSinceLevelLoad;
        }
    }

    // Shifts balls down
    protected void shift()
    {
        if(topRowHandler.transform.position.y > screenSize.y)
        {
            if (topRowHandler.transform.position.y > ball.transform.position.y + 0.3f)
            {
                topRowHandler.transform.Translate(Vector2.down * Time.deltaTime * speed * 5);
            }
            else
            {
                topRowHandler.transform.Translate(Vector2.down * Time.deltaTime * speed);
            }
        }
    }

    //Initialize all necessary fields
    protected void init()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        ballsInBigRow = Mathf.FloorToInt((screenSize.x * 2) / (ballRadius * 2)) + 1;
        leftBigRowMaxX = -(ballsInBigRow * levelPaddingX / 2 - ballRadius);
        leftSmallRowMaxX = leftBigRowMaxX + levelPaddingX / 2;
        topRowHandler.transform.position = new Vector2(0, levelPaddingY * rowsAmount);
    }

    // Main generate method
    protected GameObject generateBalls()
    {
        clearBalls();
        for (int i = 0; i < ballsInBigRow; i++)
        {
            ball = Instantiate(generation.getBall(redBall, yellowBall, greenBall, blueBall), new Vector2(leftBigRowMaxX + levelPaddingX * i, topRowHandler.transform.position.y), Quaternion.Euler(Vector3.zero));
            ball.transform.SetParent(topRowHandler.transform);
        }
        for (int i = 1; i <= rowsAmount / 2; i++)
        {
            for (int a = 0; a < ballsInBigRow - 1; a++)
            {
                Instantiate(generation.getBall(redBall, yellowBall, greenBall, blueBall), new Vector2(leftSmallRowMaxX + levelPaddingX * a, topRowHandler.transform.position.y - levelPaddingY * (i * 2 - 1)), Quaternion.Euler(Vector3.zero)).transform.SetParent(topRowHandler.transform);
            }
            for (int b = 0; b < ballsInBigRow; b++)
            {
                Instantiate(generation.getBall(redBall, yellowBall, greenBall, blueBall), new Vector2(leftBigRowMaxX + levelPaddingX * b, topRowHandler.transform.position.y - levelPaddingY * (i * 2)), Quaternion.Euler(Vector3.zero)).transform.SetParent(topRowHandler.transform);
            }
        }
        return ball;
    }

    // Clears all balls on screen
    private void clearBalls()
    {
        foreach(Transform child in topRowHandler.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void onPauseClick()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1.0f);
        if(Time.timeScale < 0.9f)
        {
            restart.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
        }
        else
        {
            restart.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
        }
    }

    public void onRestartClick()
    {
        if (waves > PlayerPrefs.GetInt(LevelChooser.getPrefName()))
        {
            PlayerPrefs.SetInt(LevelChooser.getPrefName(), waves);
        }
        onPauseClick();
        clearVariables();
        SceneManager.LoadScene("Main");
    }

    public void onExitClick()
    {
        if (waves > PlayerPrefs.GetInt(LevelChooser.getPrefName()))
        {
            PlayerPrefs.SetInt(LevelChooser.getPrefName(), waves);
        }
        onPauseClick();
        clearVariables();
        SceneManager.LoadScene("Menu");
    }

    private void clearVariables()
    {
        waves = 0;
        previousTime = 0.0f;
        timer = 0.0f;
    }
}
