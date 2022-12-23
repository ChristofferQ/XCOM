using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;

     public void SetValueShield(int Shield)
    {
        slider.value = Shield;
    }

    public void SetShield(int Shield)
    {
        slider.value = Shield;
    }
}
