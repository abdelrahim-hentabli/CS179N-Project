using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Msg : MonoBehaviour
{
    public GameObject uiObject;

    void Start() {
    	uiObject.SetActive(false);    
    }

    void OnTriggerEnter2D(Collider2D player) {
    	if(player.gameObject.CompareTag("Player")) {
    		uiObject.SetActive(true);
    		StartCoroutine("MsgDisappear");
    	}
    }

     IEnumerator MsgDisappear() {
     	yield return new WaitForSeconds(10);
     	Destroy(uiObject);
     	Destroy(gameObject);
     }



   
}
