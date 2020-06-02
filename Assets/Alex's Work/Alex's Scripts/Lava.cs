using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    GameObject thePlayer;
    private PlayerController playerController;
    public BoxCollider2D lavaTrigger;

    private float damageTimer;
    private bool inLava;

    public int lavaDamage;

    private GameObject body;

    // Start is called before the first frame update
    private void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerController = thePlayer.GetComponent<PlayerController>();
        inLava = false;
        damageTimer = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (inLava == true)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                //Debug.Log("Taking 5 damage!");
                body.SendMessageUpwards("TakeDamage", lavaDamage);
                damageTimer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Stuck in lava!");
            body = collision.gameObject;
            playerController.speed = 0.5f;
            inLava = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Got out of lava!");
            body = null;
            inLava = false;
            damageTimer = 0.0f;
            playerController.speed = 2.0f;
        }
    }
}
