using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastMelee_behavior : MonoBehaviour {
    #region Public Variables
	public Transform rayCast;
	public LayerMask rayCastMask;
	public float rayCastLength;
	public float attackDistance; //min distance for attack
	public float moveSpeed;
	public float timer; //timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
	#endregion

	#region Private Variables
	private RaycastHit2D hit;
	private Transform target;
	private Animator anim;
	private float distance; //store the distance betwn enemy and player
	private bool attackMode;
	private bool inRange; //check if player is in range
	private bool cooling; //check if enemy is cooling after attack
	private float intTimer;
	#endregion

	void Awake() {
        SelectTarget();
		intTimer = timer; //store the initial value of timer
		anim = GetComponent<Animator>();
	}


    void Update() {
        if(!attackMode) {
            Move();
        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("fastMelee_attack")) {
            SelectTarget();
        }

        if(inRange) {
        	hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
        	RaycastDebugger();
        }

        //when player is detected
        if(hit.collider != null) {
        	EnemyLogic();
        }

        else if(hit.collider == null) {
        	inRange = false;
        }

        if(inRange == false) {
        	StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig) {
    	if(trig.gameObject.tag == "Player") {
    		target = trig.transform;
    		inRange = true;
            Flip();
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

    void RaycastDebugger() {
    	if(distance > attackDistance) {
    		Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
    	}

    	else if(attackDistance > distance) {
    		Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
    	}
    }

    public void TriggerCooling() {
        cooling = true;
    }

    private bool InsideofLimits() {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget() {
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

    private void Flip() {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x) {
            rotation.y = 180f;
        }

        else {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
