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

    public AudioClip hit;
    public AudioClip death;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSrc = GetComponent<AudioSource>();
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        audioSrc.PlayOneShot(hit);


        if (currentHealth <= 0) {
            audioSrc.PlayOneShot(death);
            enemy.SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
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
