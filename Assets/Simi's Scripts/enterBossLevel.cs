using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterBossLevel : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D player) {
    	if(player.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) {
    		SceneManager.LoadScene("BossScene");
    		Debug.Log("Loading...");
    	}
    }
    
}
