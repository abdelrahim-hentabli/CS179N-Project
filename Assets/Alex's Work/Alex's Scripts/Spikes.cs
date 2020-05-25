using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //Used to get player's health
    private GameObject thePlayer;
    private PlayerController playerController;
    public BoxCollider2D spikes;

    private float debugTimer;
    private bool debugOnSpikes;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerController = thePlayer.GetComponent<PlayerController>();
        debugTimer = 0.0f;
    }

    //ONLY USING THIS FOR DEBUG.LOG STAGE
    //DELETE ONCE PLAYER HEALTH IS IMPLEMENTED
    private void Update()
    {
        if (debugOnSpikes == true)
        {
            debugTimer += Time.deltaTime;
            if (debugTimer >= 5.0f)
            {
                Debug.Log("YOU DIED");
                debugTimer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Only here to see if it detects properly
            Debug.Log("YOU DIED");
            debugOnSpikes = true;

            //Code to instantly kill player here
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GOT AWAY FROM SPIKES");
            debugOnSpikes = false;
            debugTimer = 0.0f;
        }
    }
    /*
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Only here to see if it detects properly
            Debug.Log("YOU DIED");
            debugOnSpikes = true;

            //Code to instantly kill player here
        }
    }

    //ONLY USING THIS FOR DEBUG.LOG STAGE
    //DELETE ONCE PLAYER HEALTH IS IMPLEMENTED
    private void OnTriggerExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GOT AWAY FROM SPIKES");
            debugOnSpikes = false;
            debugTimer = 0.0f;
        }
    }*/
}
