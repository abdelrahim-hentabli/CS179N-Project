using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
	//Health variables
	public int maxHealth;
	public int currentHealth;
	public Animator playerAnim;

    //Melee variables
    public Animator attack;
    public Transform attackPoint;
    public float attackRange;

    public LayerMask enemyLayers;
    public int attackDamage = 20;

    //Range variables
    public Animator shoot;
    public Transform firePoint;
    public GameObject boltPrefab;
    public GameObject thunderboltPrefab;

    //Makes sure player can't stunlock enemies to death that easily
    public float attackRate = 0.1f;
    float nextAttack = 0f;

    //Used to get access to PlayerController variables
    private GameObject thePlayer;
    private PlayerController playerController;
    private bool combatGround = true;

    //EXPERIMENTAL: Stamina counter to determine number of attacks
    //Stamina should recover as long as the player has less than 100 stamina AND has waited a certain amount of time after attacking
    //Player should not be able to attack if they have no stamina
    //public float combatStamina; //Amount of stamina
    //public float staminaTimer; //Waits between attacks before regenerating
    //public bool hasStamina; //Does the player have stamina?
    //public bool isRecovering; //Checks to see if player is recovering stamina
	// Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerController = thePlayer.GetComponent<PlayerController>();

        //EXPERIMENTAL
        //combatStamina = 100.0f;
        //staminaTimer = 0.0f;
        //isRecovering = false;
        //hasStamina = true;
    }

    void Awake()
    {
    	currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if player is grounded or not based on PlayerController script
        combatGround = playerController.isGrounded;

        //Only register attacks after a certain amount of time has passed between attacks
        if (Time.time >= nextAttack)
        {
            //Melee is left mouse click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (combatGround == true) { playerController.speed = 0f; } //If player is on the ground, stop moving
                playAnimation();
                nextAttack = Time.time + 1f / attackRate;
            }

            //Ranged is right mouse click
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (combatGround == true) { playerController.speed = 0f; } //If player is on the ground, stop moving
                playShoot();
                nextAttack = Time.time + 1f / attackRate;
            }

            //TESTING: Used for thunderbolt
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (combatGround == true) { playerController.speed = 0f; }
                playThunderbolt();
                nextAttack = Time.time + 1f / attackRate;
            }
        }

        if(currentHealth <= 0)
        {
        	playerAnim.SetBool("IsDead", true);	
        }
    }

    //Issue: Animation shows a windup, but typically the enemy would be shown as getting hit WHILE the player character winds up
    //Solution: Play the animation first and delay everything else after it by a very short amount of time, just enough
    //          for the animation to finish the windup part BEFORE swinging the sword, at which point it should register damage.
    //Note: May cause issues where enemy takes damage after moving out of range of melee attack.
    void playAnimation()
    {
        //Begin attack animation
        attack.SetTrigger("Attack");
        //Delay the mechanics of melee combat by 0.30625 seconds
        Invoke("Attack", 0.30625f);
    }

    //Same as the function above, except for the crossbow mechanic
    void playShoot()
    {
        shoot.SetTrigger("Shoot");
        Invoke("Shoot", 0.30f);
    }

    //TESTING: Launches a thunderbolt instantaneously
    void playThunderbolt()
    {
        //Summon thunderbolt
        Instantiate(thunderboltPrefab, firePoint.position, firePoint.rotation);
        Invoke("MoveAfterShoot", 0.3f);
    }

    //Melee attack
    void Attack()
    {
        //Check for enemies within the hitbox
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemy OR destroy object
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject.tag == "Enemy")
            {
            	enemy.gameObject.SendMessage("takeDamage", attackDamage);
            }
        }
        Invoke("MoveAfterAttack", 0.3f);
    }

    //Crossbow attack
    void Shoot()
    {
        //Fire a bolt
        Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
        Invoke("MoveAfterShoot", 0.3f);
    }
    
    //Lets player move after attacking
    void MoveAfterAttack()
    {
        playerController.speed = 2.0f;
    }

    //Lets player move after shooting
    void MoveAfterShoot()
    {
        playerController.speed = 2.0f;
    }
    
    //Only for melee, makes sure enemy is within range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if (damage > currentHealth)
        {
            damage = currentHealth;
        }

    	if(currentHealth > 0)
    	{
    		playerAnim.SetTrigger("Hit");
    		currentHealth -= damage;
    	}
    }

    void Death()
    {
    	Destroy(gameObject);

    	//Destroy(this.gameObject);
    }
}
