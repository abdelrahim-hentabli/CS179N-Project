using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Skull_AI : MonoBehaviour
{
	public float attackDistance;	// Minimum distance for attack
	public float moveSpeed;
	public float timer;		// Timer for cooldown between attacks
	public Transform leftLimit;
	public Transform rightLimit;
	[HideInInspector] public Transform target;
	[HideInInspector] public bool inRange;	// Check if Player is in range
	public GameObject hotZone;
	public GameObject triggerArea;

    private Animator anim;
    private float distance;	// Stire the distance between enemy and player
    private bool attackMode;
    private bool cooling;	// Check if Enemy is cooling after attack
    private float intTimer;	

    void Awake()
    {
    	SelectTarget();
    	intTimer = timer;	// Store the initial value of timer
    	anim = GetComponent<Animator>();
    }

    void Update()
    {

        if(!anim.GetBool("Dead")){
        	if(!attackMode)
        	{
        		Move();
        	}

        	if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Skull_Attack"))
        	{
        		SelectTarget();
        	}

            if(inRange)
            {
            	EnemyLogic();
            }
        }
        else if(anim.GetBool("Dead"))
        {
            anim.SetBool("Attack", false);
            anim.SetBool("CanMove", false);
        }
    } 

    void EnemyLogic()
    {
    	distance = Vector2.Distance(transform.position, target.position);

    	if(distance > attackDistance)
    	{
    		StopAttack();
    	}
    	else if (attackDistance >= distance && cooling == false)
    	{
    		Attack();
    	}

    	if(cooling)
    	{
    		Cooldown();
    		anim.SetBool("Attack", false);
    	}
    }

    void Move()
    {
    	anim.SetBool("CanMove", true);

    	if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Skull_Attack"))
    	{
    		Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

    		transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    	}
    }

    void Attack()
    {
    	timer = intTimer;	// Reset Timer when Player enter Attack Range
    	attackMode = true; 	// To check if Enemy can still attack or not

    	anim.SetBool("CanMove", false);
    	anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
    	timer -= Time.deltaTime;

    	if(timer <= 0 && cooling && attackMode)
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

    private bool InsideofLimits()
    {
    	return (transform.position.x > leftLimit.position.x) && (transform.position.x < rightLimit.position.x);
    }

    public void SelectTarget()
    {
    	float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
    	float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

    	if(distanceToLeft > distanceToRight)
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
    	if(transform.position.x > target.position.x)
    	{
    		rotation.y = 0f;
    	}
    	else
    	{
    		rotation.y = 180f;	
    	}

    	transform.eulerAngles = rotation;
    }
}
