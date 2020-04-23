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
    public float hitRate = 2f;
    float nextAttack = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttack = Time.time + 1f / hitRate;
            }
        }
    }

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
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
