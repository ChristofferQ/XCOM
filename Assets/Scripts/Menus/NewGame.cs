using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void Small()
    {
    }


    public void PlayGame()
   {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
   }
    
}
