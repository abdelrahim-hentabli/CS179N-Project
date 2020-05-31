using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //min distance for attack
    public float moveSpeed;
    public float timer; //timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotzone;
    public GameObject triggerArea;
    public GameObject head;
    public GameObject feet;
    public GameObject hitbox;
    public Collider2D body;
    public int maxHealth;
    public int currentHealth;
    public bool attackMode;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private float distance; //store the distance betwn enemy and player
    //private bool attackMode;
    private bool cooling; //check if enemy is cooling after attack
    private float intTimer;
    #endregion

    float stunTimer;
    bool stunned = false;
    

    void Awake()
    {
        SelectTarget(); //patrolling only
        intTimer = timer; //store the initial value of timer
        anim = GetComponent<Animator>();
        stunTimer = 0;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        //Next two if statements only for patrolling enemies
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Tank_Attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }

        //If enemy is stunned, do this
        if (stunned)
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= 0.2f)
            {
                stunned = false;
                stunTimer = 0;
                moveSpeed = 0.2f;
            }
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }

        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Tank_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y); //For patrolling enemies
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //reset timer when player enters attack range
        attackMode = true; //to check if enemy can still attack or nah

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    //Next two functions only for patrolling
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    //Enemy health and stuff
    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.enabled = false;
            moveSpeed = 0;

            Destroy(head);
            Destroy(feet);
            Destroy(hitbox);
            body.enabled = false;
            Destroy(hotzone);
            Destroy(triggerArea);

        }

        else
        {
            anim.SetTrigger("Hit");
            moveSpeed = 0;
            stunned = true;
        }
    }
}
