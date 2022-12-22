using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMenu : MonoBehaviour
{
    public void Move()
    {
        
    }

    public void Attack()
   {
        PlayerManager.Instance.performCombat();
   }
}
