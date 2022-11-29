using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public void PlayAgain()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
   }

    public void BackToMainMenu()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
   }
   public void QuitGame()
    {
        Application.Quit();
    }
}
