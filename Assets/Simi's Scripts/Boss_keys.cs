using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_keys : MonoBehaviour {
	public GameObject uiObject;

    void Start() {
    	uiObject.SetActive(false);    
    }

    void OnTriggerEnter2D(Collider2D player) {
    	if(player.gameObject.CompareTag("Player")) {
    		uiObject.SetActive(true);
    		PickupKey(player);
    	}
    }

    void PickupKey(Collider2D player) {
    	GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
    	Destroy(gameObject);
    }



}
