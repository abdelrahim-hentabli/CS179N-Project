using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    //Variables for bolt
    public float speed = 5f;
    public int boltDamage = 20;
    public Rigidbody2D leBolt;

    // Start is called before the first frame update
    void Start()
    {
        leBolt.velocity = transform.right * speed;
    }

    //If bolt hits enemy, deal damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);

        //Enemy enemy = collision.GetComponent<Enemy>();
        //if (enemy != null)
        //{
       //     enemy.takeDamage(boltDamage);
        //}
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("takeDamage", boltDamage);
            Destroy(gameObject); //Remove bolt after it hits enemy
        }
        
    }
}
