using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider Healthbar;
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        Healthbar.minValue = 0;
        Healthbar.maxValue = maxHealth;
        Healthbar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.minValue = 0;
        Healthbar.maxValue = maxHealth;
        Healthbar.value = currentHealth;
    }
}
