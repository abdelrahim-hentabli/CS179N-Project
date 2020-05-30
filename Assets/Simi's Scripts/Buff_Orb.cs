using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Orb : MonoBehaviour {
	public int multiplier = 2;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Pickup(other);
		}

	}

	void Pickup(Collider2D player) {
		//generate effect

		//apply effect to player
		Combat stats = player.GetComponent<Combat>();
		stats.attackDamage *= multiplier;

		//remove orb
		Destroy(gameObject);
	}
    
}
