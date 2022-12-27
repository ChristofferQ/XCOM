using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject gameTimer;
   public void PlayGame()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Credit()
    {
        SceneManager.LoadScene("CreditScene");
    }

   public void QuitGame()
   {
        Application.Quit();
   }

   public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        
    }
    public void showGameTimer(bool tog)
    {
        if (tog == true)
        {
            gameTimer.SetActive(true);
        }else
        {
            gameTimer.SetActive(false);
        }
    }
}
