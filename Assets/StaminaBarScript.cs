using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    public Slider staminaBar;
    public Gradient gradient;
    public Image fill;

    public void SetMaxStamina(float stamina)
    {
        staminaBar.maxValue = stamina;
        staminaBar.value = stamina;
        UpdateStaminaBarColor();  // Update color at max value
    }

    public void setStamina(float stamina)
    {
        staminaBar.value = stamina;
        UpdateStaminaBarColor();  // Update color based on current stamina
    }

    private void UpdateStaminaBarColor()
    {
        fill.color = gradient.Evaluate(staminaBar.normalizedValue);
    }
}
