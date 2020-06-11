using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterBossLevel : MonoBehaviour {
	void OnTriggerStay2D(Collider2D player) {
        if(player.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {
    		SceneManager.LoadScene("BossScene");
    		Debug.Log("Loading...");
    	}
    }

	void OnTriggerEnter2D(Collider2D player) {
    	if(player.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {
    		SceneManager.LoadScene("BossScene");
    		Debug.Log("Loading...");
    	}
    }
    
}
