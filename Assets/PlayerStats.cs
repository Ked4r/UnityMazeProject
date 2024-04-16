using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float maxStamina = 1000f;
    public float currentStamina;

    public HealthBarScript healthBarScript;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        healthBarScript.SetMaxHealth(maxHealth);
    }

    // Metoda do zadawania obra¿eñ graczowi
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBarScript.setHealth(currentHealth);
        if (currentHealth < 10)
        {
            SceneManager.LoadSceneAsync(2);
            AudioManager.Instance.PlaySFX("Death");
            AudioManager.Instance.stopMusic("Theme");
        }
    }

    // Metoda do regeneracji zdrowia
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Metoda do zu¿ycia staminy
    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    // Metoda do regeneracji staminy
    public void RegainStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }
}
