using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss_door : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D player) {
		PlayerController keys = player.GetComponent<PlayerController>();
    	if(player.gameObject.CompareTag("Player") && keys.numOfKeys == 3) {
    		RemoveDoor();	
    	}
    }

    void RemoveDoor() {
    	GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
    	Destroy(gameObject);
		SceneManager.LoadScene(sceneName:"BossScene");
    }

}
