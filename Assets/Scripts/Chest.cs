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

    void Awake()
    {
        Instance = this;
    }

    public void openChest(GameObject unit)
    {
        int powerUpPick = Random.Range(1,3);
        Debug.Log("her" + powerUpPick);
        //var unitToPowerUp = unit;

        if (powerUpPick == 1)
        {
            unit.GetComponent<Unit>().movePowerUp = true;
            //abilitys[0].sprite = moveAb;
            Debug.Log(unit.name + " got PowerUp " + powerUpPick);

        }
        if (powerUpPick == 2)
        {
            unit.GetComponent<Unit>().attackPowerUp = true;
            //abilitys[1].sprite = attackAb;
            Debug.Log(unit.name + " got PowerUp " + powerUpPick);
        }
/*         if (powerUpPick == 2)
        {
            unit.Instance.isEnhanced = true;
            abilitys[2].sprite = shieldAb;
        } */
        
    }
}
