using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testdrop : MonoBehaviour
{
    public static testdrop Instance;
    public int _width, _height, _depth;
    public void SetMap (int mapIndex)
    {
        if (mapIndex == 0) //SMALL
        {
            _width = 10;
            _height = 10;
            _depth = 10;
        } if (mapIndex == 1) //MEDIUM
        {
            _width = 15;
            _height = 15;
            _depth = 15;
        } if (mapIndex == 2) //LARGE
        {
            _width = 20;
            _height = 20;
            _depth = 20;
        }
        
    }

    public void PlayGame()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
   }
}
