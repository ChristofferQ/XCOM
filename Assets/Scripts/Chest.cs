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

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChest(GameObject unit)
    {
        unit = this;
        int powerUpPick = Random.Range(1,4);
        Debug.Log("her" + powerUpPick);

        if (powerUpPick == 1)
        {
            unit.Instance.isEnhanced = true;
            abilitys[0].sprite = moveAb;
        }
        if (powerUpPick == 2)
        {
            unit.Instance.isEnhanced = true;
            abilitys[1].sprite = attackAb;
        }
        if (powerUpPick == 2)
        {
            unit.Instance.isEnhanced = true;
            abilitys[2].sprite = shieldAb;
        }
        
    }
}
