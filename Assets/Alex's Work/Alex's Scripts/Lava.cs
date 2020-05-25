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
            if (damageTimer >= 0.5f)
            {
                Debug.Log("Taking 5 damage!");
                damageTimer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stuck in lava!");
            playerController.speed = 0.5f;
            inLava = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Got out of lava!");
            inLava = false;
            damageTimer = 0.0f;
            playerController.speed = 2.0f;
        }
    }
}
