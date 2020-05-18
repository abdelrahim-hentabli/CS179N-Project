using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastMelee_hotzoneCheck : MonoBehaviour {
    private fastMelee_behavior enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() {
    	enemyParent = GetComponentInParent<fastMelee_behavior>();
    	anim = GetComponentInParent<Animator>();
    }

    private void Update() {
    	if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("fastMelee_attack")) {
    		enemyParent.Flip();
    	}
    }

    private void OnTriggerEnter2D(Collider2D collider) {
    	if(collider.gameObject.CompareTag("Player")) {
    		inRange = true;
    	}
    }

    private void OnTriggerExit2D(Collider2D collider) {
    	if(collider.gameObject.CompareTag("Player")) {
    		inRange = false;
    		gameObject.SetActive(false);
    		enemyParent.triggerArea.SetActive(true);
    		enemyParent.inRange = false;
    		enemyParent.SelectTarger();
    	}
    }

}

