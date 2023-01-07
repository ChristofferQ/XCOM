using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject hudMenu;
    public GameObject timer;
    public GameObject endTurnButton;
    public GameObject screenLogger;
    public GameObject gameTimer;
    public bool isPaused;
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
        hudMenu.SetActive(false);
        timer.SetActive(false);
        endTurnButton.SetActive(false);
        screenLogger.SetActive(false);
        gameTimer.SetActive(false);
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
        hudMenu.SetActive(true);
        timer.SetActive(true);
        endTurnButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        hudMenu.SetActive(true);
        timer.SetActive(true);
        endTurnButton.SetActive(true);
        screenLogger.SetActive(true);
        gameTimer.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
