using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Unit : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    [Header("Shield Settings")]
    public int currentShield;
    public ShieldBar ShieldBar;
    [Header("Combat Settings")]
    public int movementSpeed = 3;
    public int attackRange;
    public int actionCount = 2;
    public int numOfActions;

    public Image[] actions;
    public Sprite fullAction;
    public Sprite emptyAction; 

    [SerializeField] public CanvasGroup stats;

    
    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        ShieldBar.SetValueShield(currentShield);

        PlayerManager.AddUnit(this); 

        stats.alpha = 0f;
    }

    void Update(){

        if (Input.GetKeyDown("x"))
        {
            TakeDamage(20);
        }

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
    void TakeDamage(int damage)
    {

        if (currentShield == 0 && currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }else if(currentShield > 0)
        {
            currentShield -= damage;
            ShieldBar.SetShield(currentShield);
        }   
    }
}


