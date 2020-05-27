using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public Rigidbody2D boulder;
    //public BoxCollider2D boulderDestroyer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // If boulder hits the player, deal damage
    // If boulder enters where it should be destroyed, destroy the boulder
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the player");
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boulder hit player");
        }
    }
}
