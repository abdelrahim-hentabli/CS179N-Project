using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Orb : MonoBehaviour {
	public int multiplier = 2;
	//public float duration = 3.0f;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			StartCoroutine(Pickup(other));
		}

	}

	 IEnumerator Pickup(Collider2D player) {
		//generate effect (not sure if this is needed yet)

		//increase player damage by x2
		Combat stats = player.GetComponent<Combat>();
		stats.attackDamage = stats.attackDamage * multiplier;

		//remove orb from scene
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;

		//wait x amount of seconds, then reverse damage stats back to normal and destroy object
		yield return new WaitForSeconds(10);
		stats.attackDamage = stats.attackDamage / multiplier;
		Destroy(gameObject);

		
	}
    
}
