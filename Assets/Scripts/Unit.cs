using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int movementSpeed = 3;
    public int actionCount = 2;
    public int numOfActions;

    public Image[] actions;
    public Sprite fullAction;
    public Sprite emptyAction; 
    
    void Start() 
    {
        PlayerManager.AddUnit(this); 
    }

    void Update(){

        if (actionCount > numOfActions)
        {
            actionCount = numOfActions;    
        }

        for (int i = 0; i < actions.Length; i++)
        {
            if (i < actionCount)
            {
                actions[i].sprite = fullAction;
            }else
            {
                actions[i].sprite = emptyAction;
            }
            if (i < numOfActions)
            {
                actions[i].enabled = true;
            }else
            {
                actions[i].enabled = false;
            }
        }
    }
}
