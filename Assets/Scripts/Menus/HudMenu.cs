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

   public void potion()
    {
        if(!PlayerManager.Instance.selectedUnit) return;
        
    }

    public void cancel()
    {
        if(!PlayerManager.Instance.selectedUnit) return;
        PlayerManager.Instance.DeselectUnit();
    }

   public void endTurn()
    {
        PlayerManager.Instance.end();
    }

}
