using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //Used to get player's health
    private GameObject thePlayer;
    private PlayerController playerController;
    public BoxCollider2D spikes;

    //private bool debugOnSpikes;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerController = thePlayer.GetComponent<PlayerController>();
    }
}
