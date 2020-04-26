using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HUD : MonoBehaviour
{
    const int HEALTH_POTION_AMOUNT = 5;
    public Slider Healthbar;
    public Text healthPotionAmount;
    public int maxHealth;
    public int currentHealth;
    public int healthPotions;
    public int potionStrength;
    // Start is called before the first frame update
    void Start()
    {
        potionStrength = 35;
        maxHealth = 100;
        currentHealth = 100;
        healthPotions = HEALTH_POTION_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();
        Healthbar.minValue = 0;
        Healthbar.maxValue = maxHealth;
        Healthbar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            onHealthPotion();
        }
        Healthbar.value = currentHealth;
    }

    public void onHit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 100;
        }
        Healthbar.value = currentHealth;
    }

    void  onHealthPotion()
    {
        if(healthPotions > 0)
        {
            healthPotions--;
            currentHealth += potionStrength;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthPotionAmount.text = healthPotions.ToString();
            Healthbar.value = currentHealth;
        }
    }

    public void replenishHealthPotions()
    {
        healthPotions = HEALTH_POTION_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();
    }
}
