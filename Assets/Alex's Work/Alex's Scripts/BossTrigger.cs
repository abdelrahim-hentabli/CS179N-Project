using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private BossBehavior bossBehavior;

    private void Awake()
    {
        bossBehavior = GetComponentInParent<BossBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bossBehavior.target = collision.transform;
            bossBehavior.inRange = true;
            bossBehavior.hotzone.SetActive(true);
        }
    }
}
