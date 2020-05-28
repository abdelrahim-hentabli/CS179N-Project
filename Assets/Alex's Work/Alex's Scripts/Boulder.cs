using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public Rigidbody2D boulder;
    public Animator anim;
    public CircleCollider2D trigger;
    public CircleCollider2D boulderCollider;
    public Transform destination;
    public float rollSpeed;
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rollSpeed = 1.0f;
    }

    void Update()
    {
        Roll();
    }

    //Determines whether boulder falls straight down or rolls towards the starting point
    void Roll()
    {
        if (grounded == true)
        {
            Vector2 destinationPosition = new Vector2(destination.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destinationPosition, rollSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 destinationPosition = new Vector2(0, -transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destinationPosition, rollSpeed * Time.deltaTime);
        }
    }

    //Next two functions check if boulder is on ground or not
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) //10 is the index of ground; change this if ground layer's index is ever changed
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            grounded = false;
        }
    }

    //When boulder runs into player, destroy its colliders and play break animation
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boulder hit player");
            rollSpeed = 0f;
            Destroy(trigger);
            Destroy(boulderCollider);
            anim.SetTrigger("HitPlayer");
            Invoke("DestroyBoulder", 0.80f);
        }
    }

    //Destroy the boulder object
    void DestroyBoulder()
    {
        Destroy(gameObject);
    }
}
