using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastMelee_triggerAreaCheck : MonoBehaviour {
    private fastMelee_behavior enemyParent;

    private void Awake() {
    	enemyParent = GetComponentInParent<fastMelee_behavior>();
    }

    private void OnTrigger2D(Collider2D collider) {
    	if(collider.gameObject.CompareTag("Player")) {
    		gameObject.SetActive(false);
    		enemyParent.target = collider.transform;
    		enemyParent.inRange = true;
    		enemyParent.hotZone.SetActive(true);
    	}
    }
}
