using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Unit : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int maxShield = 100;
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
    public bool inCombatRange = false;

    
    void Start() 
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.SetMaxHealth(maxHealth);
        ShieldBar.SetMaxShield(maxShield);

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
    public void TakeDamage(int damage)
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
        //Debug.Log("Current Health: " + currentHealth);
        //Debug.Log("Current Shield: " + currentShield);
        if (currentHealth <= 0)
        {
            Debug.Log(GetComponent<Unit>().name + " is Dead!");
            Destroy(gameObject);
            CombatManager.Instance.GameOver();
        }

    }
}


