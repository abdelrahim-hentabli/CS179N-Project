using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_item : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
            other.SendMessage("giveScrollItem");
            Pickup();
		}

	}

	void Pickup() {
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
    	Destroy(gameObject);

	}
}
