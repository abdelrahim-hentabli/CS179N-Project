using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script deals with melee combat

public class PlayerCombat : MonoBehaviour
{
    public Animator attack;

    public Transform attackPoint;
    public float attackRange = 0.25f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Play a blade-swinging animation
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
