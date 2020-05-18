using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{ 
    public GameObject leftBound;
    public GameObject rightBound;

    public GameObject player;

    public GameObject crossBow;

    private Rigidbody2D body;

    public BoxCollider2D attackBox;

    public Animator animator;

    public GameObject boltPrefab;

    public float speed;

    const float WALK_SPEED = .5f;

    const float BOLT_COOLDOWN = 1.5f;

    const float STUN_TIME = 1.5f;

    const int STARTING_HEALTH = 100;



    bool right = true;

    float pauseTimer;
    float boltTimer;
    float stunTimer;


    bool waiting = false;
    bool stunned = false;

    SeeEnemy seeEnemy;

    bool enemyInSight;

    int maxHealth;
    int currentHealth;

    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth = STARTING_HEALTH;
        body = GetComponent<Rigidbody2D>();
        seeEnemy = attackBox.GetComponent<SeeEnemy>();
        pauseTimer = 0;
        boltTimer = 0;
        enemyInSight = false;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        boltTimer += Time.deltaTime;
        enemyInSight = seeEnemy.seeEnemy;
        if (stunned)
        {
            stunTimer += Time.deltaTime;
            if(stunTimer >= STUN_TIME)
            {
                stunned = false;
                stunTimer = 0;
            }
        }
        else if (boltTimer >= BOLT_COOLDOWN && enemyInSight)
        {
            boltTimer = 0;
            animator.SetBool("Move", false);
            animator.SetBool("Attack", true);
            Invoke("shootBolt", .35f);
            speed = 0;
        }
        else if (enemyInSight)
        {
            animator.SetBool("Attack", false);
            speed = 0;
        }
        else if (boltTimer >= BOLT_COOLDOWN && waiting)
        {
            animator.SetBool("Attack", false);
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= 1.0f)
            {
                animator.SetBool("Move", true);
                pauseTimer = 0;
                speed = WALK_SPEED;
                right = !right;
                Vector3 localScale = transform.localScale;
                localScale.x = right ? 1 : -1;
                transform.localScale = localScale;
                waiting = false;
            }
        }
        else if (boltTimer >= BOLT_COOLDOWN && (right && transform.position.x >= rightBound.transform.position.x) || (!right && transform.position.x <= leftBound.transform.position.x))
        {
            animator.SetBool("Attack", false);
            waiting = true;
            animator.SetBool("Move", false);
            speed = 0;
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Move", true);
            speed = 1;
        }

        body.velocity = new Vector2( (right?1:-1) * speed, body.velocity.y);
    }

    void shootBolt()
    {
        GameObject instance = Instantiate(boltPrefab, crossBow.transform.position, transform.rotation);
        instance.GetComponent<Rigidbody2D>().velocity = new Vector3((right ? 1 : -1) * 4f, 0f, 0f);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.enabled = false;
            speed = 0;
        }
        else
        {
            animator.SetTrigger("Hit");
            speed = 0;
            stunned = true;
        }
    }
}
