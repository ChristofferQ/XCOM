using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject hudMenu;
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
        Time.timeScale = 0f;
        isPaused = true;
    }

     public void AllHerosDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void AllEnemysDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        hudMenu.SetActive(true);
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