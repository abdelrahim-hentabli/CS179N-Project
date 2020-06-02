using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_Projectile : MonoBehaviour
{
	public float speed;
	public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {	
    	if (collision.gameObject.tag == "PlayerHitbox" || collision.gameObject.layer == 13 || collision.gameObject.tag == "Wall")	
    	{
            Debug.Log(collision.gameObject.name);
    		Destroy(gameObject);
    	}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    	// Layer 13 is Wall layer
    	if(collision.gameObject.layer == 13)
    	{
    		Destroy(gameObject);
    	}
    }

}