using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float currentHealth;

    public Slider healthBarSlider;
    
    void Start()
    {
        
        currentHealth = maxHealth;

        
        if (healthBarSlider != null)
        {
           
            healthBarSlider.maxValue = maxHealth;

            
            healthBarSlider.value = currentHealth;
        }
        else
        {
           
            Debug.LogError("Ch?a kéo ??i t??ng 'Health2' (Slider) vào bi?n 'Health Bar Slider'!");
        }
    }

    
    public void TakeDamage(float damageAmount)
    {
        
        currentHealth -= damageAmount;

        
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

       
        UpdateHealthBar();
    }

    
    void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            
            healthBarSlider.value = currentHealth;
        }
    }
}