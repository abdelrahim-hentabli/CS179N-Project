using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator enemy;


    float stunTimer;
    bool isStunned = false;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        


        if (currentHealth <= 0) {
            enemy.SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.enabled = false;
            //moveSpeed = 0;
        }

        else {
            enemy.SetTrigger("Hit");
            //moveSpeed = 0;
            isStunned = true;
        }
    }
}
