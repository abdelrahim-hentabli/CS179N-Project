using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
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

    //Makes sure player can't stunlock enemies to death that easily
    public float attackRate = 2f;
    float nextAttack = 0f;

    // Update is called once per frame
    void Update()
    {
        //Only register attacks after a certain amount of time has passed between attacks
        if (Time.time >= nextAttack)
        {
            //Melee is left mouse click
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playAnimation();
                nextAttack = Time.time + 1f / attackRate;
            }
            //Ranged is right mouse click
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                playShoot();
                nextAttack = Time.time + 1f / attackRate;
            }
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

    //Melee attack
    void Attack()
    {
        //Check for enemies within the hitbox
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hitbox.position, hitboxRange, enemyLayers);

        //Damage enemy OR destroy object
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if(enemy.gameObject.tag == "Enemy") {
                enemy.gameObject.SendMessage("takeDamage", attackDamage);
            }
            //enemy.GetComponent<Enemy>().takeDamage(attackDamage);
            //enemy.GetComponent<fastMelee_behavior>().takeDamage(attackDamage);
        }
    }

    //Crossbow attack
    void Shoot()
    {
        //Fire a bolt
        Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
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
/*
    void OnTriggerEnter2D(Collider2D hitInfo) {
        fastMelee_behavior fastM = hitInfo.GetComponent<fastMelee_behavior>();
        if(fastM != null) {
            fastM.takeDamage(attackDamage);
        }
    }
*/
}
