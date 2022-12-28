using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudMenu : MonoBehaviour
{
    public void Move()
    {
        MovementManager.Instance.performMovement();   
    }

    public void Attack()
   {
        CombatManager.Instance.performCombat();
   }

   public void endTurn()
    {
        PlayerManager.Instance.end();
    }
}
