using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHotzone : MonoBehaviour
{
    private BossBehavior bossBehavior;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        bossBehavior = GetComponentInParent<BossBehavior>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss-Attack"))
        {
            bossBehavior.Flip();
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
            bossBehavior.triggerArea.SetActive(true);
            bossBehavior.inRange = false;
            bossBehavior.SelectTarget();
        }
    }
}
