using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //Melee variables
    public Animator attack;
    public Transform attackPoint;
    public float attackRange = 0.50f;
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
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
            //Ranged is right mouse click
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Shoot();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    //Melee attack
    void Attack()
    {
        //Play sword animation
        attack.SetTrigger("Attack");
        
        //Check for enemies within the hitbox
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemy OR destroy object
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
    }

    //Crossbow attack
    void Shoot()
    {
        //Play crossbow firing animation
        shoot.SetTrigger("Shoot");
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
}
