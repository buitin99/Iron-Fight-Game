using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Slider slider;

    public void CreateHealthBar(float maxHealth)
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHealth;
        slider.value    = maxHealth;
    }

   public void UpdateHealthBarValue(float health)
   {
        slider.value = health;
   }
}
