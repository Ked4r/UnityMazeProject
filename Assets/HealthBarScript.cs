using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Slider healthBar;

    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        healthBar.value = health;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }


}
