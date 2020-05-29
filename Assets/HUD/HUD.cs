using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUD : MonoBehaviour
{
    const int HEALTH_POTION_STARTING_AMOUNT = 5;
    const int BOMB_STARTING_AMOUNT = 3;
    const int NUMBER_OF_QUICK_ITEMS = 1;
    const int MAX_BOLTS = 6;

    public Slider Healthbar;
    public Text healthPotionAmount;
    public Image[] bolts = new Image[6];

    public Text bombAmount;

    public int maxHealth;
    public int currentHealth;
    public int healthPotions;
    public int potionStrength;

    public int currentQuickItem;

    public int bombs;

    public int boltAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        potionStrength = 35;
        maxHealth = 100;
        currentHealth = 100;
        healthPotions = HEALTH_POTION_STARTING_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();

        currentQuickItem = 0;

        bombs = BOMB_STARTING_AMOUNT;
        bombAmount.text = bombs.ToString();

        boltAmount = MAX_BOLTS;

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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            onCrossbow();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if(currentQuickItem == 0)
            {
                onBomb();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentQuickItem++;
            currentQuickItem %= NUMBER_OF_QUICK_ITEMS;
            updateQuickItems();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            onHit(22);
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

    public void  onHealthPotion()
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

    public void onBomb()
    {
        if(bombs > 0)
        {
            bombs--;
            bombAmount.text = bombs.ToString();
        }
    }

    public void replenishHealthPotions()
    {
        healthPotions = HEALTH_POTION_STARTING_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();
    }

    public void updateQuickItems()
    {
         
    }

    public void onCrossbow()
    {
        if(boltAmount > 0)
        {
            boltAmount--;
            bolts[boltAmount].enabled = false;
        }
    }
}
