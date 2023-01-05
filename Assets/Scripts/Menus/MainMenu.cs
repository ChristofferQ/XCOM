using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject gameTimer;
    public Toggle props;
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

    public void onProps()
    {
        if (props.isOn)
        {
            Debug.Log("on");
            GridManager.props = true;
        }
        else
        {
            Debug.Log("off");
            GridManager.props = false;
        }
    }
    public void showGameTimer(bool tog)
    {
        if (tog == true)
        {
            gameTimer.SetActive(true);
        }
        else
        {
            gameTimer.SetActive(false);
        }
    }
}
