using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastMelee_behavior : MonoBehaviour {
    #region Public Variables
	public float attackDistance; //min distance for attack
	public float moveSpeed;
	public float timer; //timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //check if player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    public int maxHealth;
    public static int currentHealth;
	#endregion

	#region Private Variables
    const int STARTING_HEALTH = 100;
    //const float STUN_TIME = 1.5f;
	private Animator anim;
	private float distance; //store the distance betwn enemy and player
	private bool attackMode;
	private bool cooling; //check if enemy is cooling after attack
	private float intTimer;
    float stunTimer;
    bool isStunned = false;
	#endregion

	void Awake() {
        SelectTarget();
		intTimer = timer; //store the initial value of timer
		anim = GetComponent<Animator>();
        stunTimer = 0;
	}

    void Start() {
        currentHealth = maxHealth;
    }

    void Update() {
        if(!anim.GetBool("Dead")) {
            if(!attackMode) {
                Move();
            }

            if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("fastMelee_attack")) {
                SelectTarget();
            } 

 
            if(inRange) {
                EnemyLogic();
            }
        }

        else if(anim.GetBool("Dead")) {
            anim.SetBool("Attack", false);
            anim.SetBool("canWalk", false);
            Destroy(hotZone);
            Destroy(triggerArea);
        }
        

        if(isStunned) {
            stunTimer += Time.deltaTime;
            if(stunTimer >= 0.5f) {
                isStunned = false;
                stunTimer = 0;
                moveSpeed = 0.2f;
            }

        }
    }

    void EnemyLogic() {
    	distance = Vector2.Distance(transform.position, target.position);

    	if(distance > attackDistance) {
    		StopAttack();
    	}

    	else if(attackDistance >= distance && cooling == false) {
    		Attack();
    	}

    	if(cooling) {
            Cooldown();
    		anim.SetBool("Attack", false);
    	}
    }

    void Move() {
        anim.SetBool("canWalk", true);

    	if(!anim.GetCurrentAnimatorStateInfo(0).IsName("fastMelee_attack")) {
    		Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
    		transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    	}
    }

    void Attack() {
    	timer = intTimer; //reset timer when player enters attack range
    	attackMode = true; //to check if enemy can still attack or nah

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown() {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode) {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack() {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }



    public void TriggerCooling() {
        cooling = true;
    }

    private bool InsideofLimits() {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget() {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight) {
            target = leftLimit;
        }

        else {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip() {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x) {
            rotation.y = 180f;
        }

        else {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public void reanimate()
    {

    }
/*
    public void takeDamage(int damage) {
        currentHealth -= damage;
        

        if (currentHealth <= 0) {
            anim.SetBool("Dead", true);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.enabled = false;
            moveSpeed = 0;
        }

        else {
            anim.SetTrigger("Hit");
            moveSpeed = 0;
            isStunned = true;
        }
    }
*/
}

