using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Unit : MonoBehaviour
{
    public static Unit Instance;
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public int potionUsage = 2;
    [Header("Shield Settings")]
    public int maxShield = 100;
    public int currentShield;
    public ShieldBar ShieldBar;
    [Header("Combat Settings")]
    public int movementSpeed = 3;
    public int attackRange;
    public int actionCount = 2;
    public int numOfActions;

    public bool attackPowerUp = false;
    public bool movePowerUp = false;
    public bool shieldPowerUp = false;

    Animator animator;

    public Image[] actions;
    public Sprite fullAction;
    public Sprite emptyAction; 

    [SerializeField] public CanvasGroup stats;
    public bool inCombatRange = false;

    
    void Start() 
    {
        Instance = this;
        currentHealth = maxHealth;
        currentShield = 0;
        healthBar.SetMaxHealth(maxHealth);
        ShieldBar.SetMaxShield(0);

        PlayerManager.AddUnit(this); 

        stats.alpha = 0f;

        animator = GetComponent<Animator>();
    }

    void Update(){

        //PlayerManager.Instance.ShowAllStatsBar();
        //PlayerManager.Instance.showStatsBar(PlayerManager.Instance.selectedUnit);

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

        animator.SetFloat("BlendSpeed", GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude);
    }
    public void TakeDamage(int damage)
    {
        if (currentShield <= 0 && currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }else if(currentShield > 0)
        {
            currentShield -= damage;
            ShieldBar.SetShield(currentShield);
        }
        if (currentHealth <= 0)
        {
            Debug.Log(GetComponent<Unit>().name + " was killed");
            gameObject.SetActive(false);
        }
        CombatManager.Instance.CheckGameOver();
    }

    public void healing(int heal)
    {
        if (PlayerManager.Instance.selectedUnit.GetComponent<Unit>().potionUsage > 0)
        if(PlayerManager.Instance.selectedUnit.GetComponent<Unit>().currentHealth < 100)
        {
            PlayerManager.Instance.selectedUnit.GetComponent<Unit>().currentHealth += heal;
            PlayerManager.Instance.selectedUnit.GetComponent<Unit>().potionUsage--;
            PlayerManager.Instance.selectedUnit.GetComponent<Unit>().healthBar.SetHealth(PlayerManager.Instance.selectedUnit.GetComponent<Unit>().currentHealth);
            Debug.Log("plzzzz"+ PlayerManager.Instance.selectedUnit.GetComponent<Unit>().currentHealth);
            
        }else if (PlayerManager.Instance.selectedUnit.GetComponent<Unit>().maxHealth == 100)
        {
            Debug.Log("You have full health");
        }
        
    }
}


