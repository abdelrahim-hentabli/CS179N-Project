using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_keys : MonoBehaviour {
	public GameObject uiObject;
	public int numKeys = 0;

    void Start() {
    	uiObject.SetActive(false);    
    }

    void OnTriggerEnter2D(Collider2D player) {
    	if(player.gameObject.CompareTag("Player")) {
    		uiObject.SetActive(true);
    		numKeys++;
    		PickupKey(player);
    	}
    }

    void PickupKey(Collider2D player) {
    	PlayerController keys = player.GetComponent<PlayerController>();
    	keys.numOfKeys = numKeys;
    	GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
    	Destroy(gameObject);
    }



}
