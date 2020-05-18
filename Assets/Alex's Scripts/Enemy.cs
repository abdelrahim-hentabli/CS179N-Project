using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator enemy;

    public int maxHealth;
    public static int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        enemy.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            //Die
            //Debug.Log("Enemy dead");

            enemy.SetBool("Dead", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
}
