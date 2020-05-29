using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_System : MonoBehaviour
{
	public int maxDamage;
	
	private int damage;

	void Awake()
	{
		damage = maxDamage;
	}

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "PlayerHitbox")
        {
            collision.gameObject.SendMessageUpwards("TakeDamage", damage);
        }
    }
}
