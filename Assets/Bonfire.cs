using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController.isGrounded && playerController.isCrouched && timer >= 10f)
        {
            timer = 0.0f;
            collision.gameObject.SendMessage("save");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController.isGrounded && playerController.isCrouched && timer >= 10f)
        {
            timer = 0.0f;
            collision.gameObject.SendMessage("save");
        }
    }
}
