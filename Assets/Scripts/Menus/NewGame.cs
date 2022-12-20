using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    

    public void Small()
    {
        GridManager.Instance._depth = 10;
        GridManager.Instance._height = 1;
        GridManager.Instance._width = 10;

    }


    public void PlayGame()
   {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
   }
    
}
