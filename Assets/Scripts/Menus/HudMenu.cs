using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudMenu : MonoBehaviour
{
    public void Move()
    {
        if(!PlayerManager.Instance.selectedUnit) return;
        MovementManager.Instance.performMovement();   
    }

    public void Attack()
   {
        if(!PlayerManager.Instance.selectedUnit) return;
        CombatManager.Instance.performCombat();
   }

   public void endTurn()
    {
        PlayerManager.Instance.end();
    }
}
