using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public static Chest Instance; 
    public Image[] abilitys;
    public Sprite moveAb;
    public Sprite attackAb;
    public Sprite shieldAb;
    public Animator animator;

    public bool isOpen = false;

    void Awake()
    {
        Instance = this;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void openChest(GameObject unit)
    {
        if (isOpen == false)
        {
            animator.Play("Open");
            int powerUpPick = Random.Range(1,4);
            if (powerUpPick == 1)
            {
                unit.GetComponent<Unit>().movePowerUp = true;
                //abilitys[0].sprite = moveAb;
                unit.GetComponent<Unit>().movementSpeed = 5;
                Debug.Log(unit.name + "'s movement was empowered");
            }
            if (powerUpPick == 2)
            {
                unit.GetComponent<Unit>().attackPowerUp = true;
                //abilitys[1].sprite = attackAb;
                Debug.Log(unit.name + "'s attack was empowered");
            }
            if (powerUpPick == 3)
            {
                unit.GetComponent<Unit>().shieldPowerUp = true;
                unit.GetComponent<Unit>().currentShield = 100;
                unit.GetComponent<Unit>().ShieldBar.SetMaxShield(100);
                //abilitys[2].sprite = shieldAb;
                Debug.Log(unit.name + " found a shield"); 
            }
        } else {
            Debug.Log("Chest has already been opened");
        }
    }
}
