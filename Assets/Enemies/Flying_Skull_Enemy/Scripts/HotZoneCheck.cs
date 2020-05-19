using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Flying_Skull_AI enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
    	enemyParent = GetComponentInParent<Flying_Skull_AI>();
    	anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
    	if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Flying_Skull_Attack"))
    	{
    		enemyParent.Flip();
    	}
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
    	if(collider.gameObject.CompareTag("Player"))
    	{
    		inRange = true;
    	}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
    	if(collider.gameObject.CompareTag("Player"))
    	{
    		inRange = false;
    		gameObject.SetActive(false);
    		enemyParent.triggerArea.SetActive(true);
    		enemyParent.inRange = false;
    		enemyParent.SelectTarget();
    	}
    }
}
