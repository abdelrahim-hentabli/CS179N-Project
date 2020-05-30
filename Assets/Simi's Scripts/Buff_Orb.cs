using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Orb : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Pickup();
		}

	}

	void Pickup() {
		//generate effect

		//apply effect to player

		//remove orb
		Destroy(gameObject);
	}
    
}
