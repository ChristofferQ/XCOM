using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   }

    public void BackToMainMenu()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
   }
   public void QuitGame()
    {
        Application.Quit();
    }
}
