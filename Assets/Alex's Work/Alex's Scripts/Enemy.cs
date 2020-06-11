using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator enemy;
    public GameObject enemyObject;
    public int maxHealth = 100;
    
    private GameObject hitboxObject;
    private fastMelee_behavior fastMeleeScript;
    private float stunTimer;
    private bool isStunned = false;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        hitboxObject = enemyObject.transform.Find("fastMelee_hitbox").gameObject;
        currentHealth = maxHealth;
        stunTimer = 0;
        fastMeleeScript = enemyObject.GetComponent<fastMelee_behavior>();
    }

    void Update()
    {
        //If enemy is stunned, do this
        if (isStunned)
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= 0.2f)
            {
                isStunned = false;
                stunTimer = 0;
                fastMeleeScript.moveSpeed = 1f;
            }
        }
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            enemy.SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            hitboxObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
            this.enabled = false;
            fastMeleeScript.moveSpeed = 0;
        }

        else {
            enemy.SetTrigger("Hit");
            fastMeleeScript.moveSpeed = 0;
            isStunned = true;
        }
    }
}
