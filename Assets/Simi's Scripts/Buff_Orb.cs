using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Orb : MonoBehaviour {
	public int multiplier = 2;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
            other.SendMessage("giveBuffItem");
            Pickup();
		}

	}

	void Pickup() {
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
    	Destroy(gameObject);

	}
    
}
