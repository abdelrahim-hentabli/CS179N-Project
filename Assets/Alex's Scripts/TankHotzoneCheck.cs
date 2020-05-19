using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHotzoneCheck : MonoBehaviour
{
    private TankBehavior tankParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        tankParent = GetComponentInParent<TankBehavior>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Tank_Attack"))
        {
            tankParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            tankParent.triggerArea.SetActive(true);
            tankParent.inRange = false;
            tankParent.SelectTarget();
        }
    }
}
