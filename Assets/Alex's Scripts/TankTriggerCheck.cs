using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTriggerCheck : MonoBehaviour
{
    private TankBehavior tankParent;

    private void Awake()
    {
        tankParent = GetComponentInParent<TankBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            tankParent.target = collision.transform;
            tankParent.inRange = true;
            tankParent.hotzone.SetActive(true);
        }
    }
}
