using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Skull_Damage : MonoBehaviour
{
    public Animator enemy;
    public GameObject enemyObject;
    public int maxHealth = 100;
    
    private Flying_Skull_AI skullScript;
    private int currentHealth;
    private float stunTimer;
    private bool stunned = false;

    private Animator anim;

    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        audioSrc = GetComponent<AudioSource>();

        stunTimer = 0;
        skullScript = enemyObject.GetComponent<Flying_Skull_AI>();
    }

    void Update()
    {
        //If enemy is stunned, do this
        if (stunned)
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= 0.2f)
            {
                stunned = false;
                stunTimer = 0;
                skullScript.moveSpeed = 0.5f;
            }
        }
    }

    public void takeDamage(int damage)
    {
        audioSrc.PlayOneShot(hitSound);
        currentHealth -= damage;

        // Play hurt animation
        // enemy.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            //Die
            //Debug.Log("Enemy dead");
            audioSrc.PlayOneShot(deathSound);
            anim.SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            // enemy.SetBool("Dead", true);
            // GetComponent<Collider2D>().enabled = false;
            // this.enabled = false;
        }
        else
        {
            anim.SetTrigger("Hit");
            skullScript.moveSpeed = 0;
            stunned = true;     
        }
    }

    public void reanimate()
    {
        currentHealth = 100;
        anim.SetBool("Dead", false);
        anim.SetBool("CanMove", true);
        anim.SetBool("Attack", false);
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<Collider2D>().enabled = true;
        this.enabled = true;
    }

}
