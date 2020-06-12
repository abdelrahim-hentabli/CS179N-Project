using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_item : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.SendMessage("giveBombItem");
            Pickup();
        }

    }
    void Pickup()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);

    }
}
