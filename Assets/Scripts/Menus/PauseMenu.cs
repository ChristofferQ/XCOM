using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject pauseMenu;
    public GameObject HudMenus;
    public CanvasGroup HerosHudMenu;
    public CanvasGroup EnemysHudMenu;
    public GameObject timer;
    public CanvasGroup gameTimer;
    public CanvasGroup gameTimerToggle;
    public GameObject log;
    public CanvasGroup logToggle;
    public GameObject endTurnButton;
    public bool isPaused;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        HudMenus.SetActive(false);
        HerosHudMenu.alpha = 0f;
        EnemysHudMenu.alpha = 0f;
        endTurnButton.SetActive(false);
        log.SetActive(false);
        logToggle.alpha = 0f;
        gameTimerToggle.alpha = 0f;
        gameTimer.alpha = 0f;
        timer.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        PlayerManager.Instance.DeselectUnit();
    }

     public void AllHerosDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void AllEnemysDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.SetActive(false);
        timer.SetActive(true);
        endTurnButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        HudMenus.SetActive(true);
        HerosHudMenu.alpha = 1f;
        EnemysHudMenu.alpha = 1f;
        endTurnButton.SetActive(true);
        log.SetActive(true);
        logToggle.alpha = 1f;
        gameTimer.alpha = 1f;
        gameTimerToggle.alpha = 1f;
        timer.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void showGameTimer(bool tog)
    {
        if (tog == true)
        {
            gameTimer.alpha = 1f;
        }
        else
        {
            gameTimer.alpha = 0f;
        }
    }

    public void showScreenLogger(bool tog)
    {
        if (tog == true)
        {
            AClockworkBerry.ScreenLogger.ShowLog = true;
        }
        else
        {
           AClockworkBerry.ScreenLogger.ShowLog = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
