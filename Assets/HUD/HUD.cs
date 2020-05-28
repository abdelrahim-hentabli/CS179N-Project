using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HUD : MonoBehaviour
{
    const int HEALTH_POTION_STARTING_AMOUNT = 5;
    const int BOMB_STARTING_AMOUNT = 3;
    const int SCROLL_STARTING_AMOUNT = 1;
    const int NUMBER_OF_QUICK_ITEMS = 2;
    const int MAX_BOLTS = 6;
    const int HUD_ELEMENTS = 4;

    public Text levelTimer;

    public Slider Healthbar;
    public Text healthPotionAmount;
    public Image[] bolts = new Image[6];
    public Image[] quickItems = new Image[2];
    public GameObject gameOverScreen;

    public GameObject[] hudElements = new GameObject[4];

    public Text quickItemAmount;

    public int maxHealth;
    public int currentHealth;
    public int healthPotions;
    public int potionStrength;

    public int currentQuickItem;

    public int[] itemAmount;

    public int boltAmount;

    bool alive;

    public float currentLevelTime;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelTime = 0.0f;
        alive = true;
        gameOverScreen.SetActive(false);
        potionStrength = 35;
        maxHealth = 100;
        currentHealth = 100;
        healthPotions = HEALTH_POTION_STARTING_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();

        currentQuickItem = 0;

        itemAmount = new int[2];
        itemAmount[0] = BOMB_STARTING_AMOUNT;
        itemAmount[1] = SCROLL_STARTING_AMOUNT;
        quickItemAmount.text = itemAmount[currentQuickItem].ToString();

        boltAmount = MAX_BOLTS;

        for (int i = 0; i < NUMBER_OF_QUICK_ITEMS; i++)
        {
            quickItems[i].enabled = false;
        }
        quickItems[currentQuickItem].enabled = true;
        Healthbar.minValue = 0;
        Healthbar.maxValue = maxHealth;
        Healthbar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelTime += Time.deltaTime;
        int currentMinutes = (int)(currentLevelTime / 60);
        levelTimer.text = currentMinutes.ToString("00") +":" + (currentLevelTime - 60 * currentMinutes).ToString("00.00");
        if (alive)
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
                if (itemAmount[currentQuickItem] > 0)
                {
                    itemAmount[currentQuickItem]--;
                    quickItemAmount.text = itemAmount[currentQuickItem].ToString();
                    if (currentQuickItem == 0)
                    {
                        onBomb();
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                updateQuickItems();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                onHit(22);
            }

            Healthbar.value = currentHealth;
        }
    }

    public void onHit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            onDeath();
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
        
    }

    public void replenishHealthPotions()
    {
        healthPotions = HEALTH_POTION_STARTING_AMOUNT;
        healthPotionAmount.text = healthPotions.ToString();
    }

    public void updateQuickItems()
    {
        quickItems[currentQuickItem].enabled = false;
        currentQuickItem++;
        currentQuickItem %= NUMBER_OF_QUICK_ITEMS;
        quickItems[currentQuickItem].enabled = true;
        quickItemAmount.text = itemAmount[currentQuickItem].ToString();
    }

    public void onCrossbow()
    {
        if(boltAmount > 0)
        {
            boltAmount--;
            bolts[boltAmount].enabled = false;
        }
    }

    public void onDeath()
    {
        for(int i = 0; i < HUD_ELEMENTS; i++)
        {
            hudElements[i].SetActive(false);
        }
        gameOverScreen.SetActive(true);
        alive = false;
    }
}
