using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : MonoBehaviour
{
    public float speed;
    public int damage = 50;
    public Rigidbody2D thunderbolt;
    public Animator anim;

    public float traveled = 0f;
    Vector2 lastPosition;
    private bool hitWall = false;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        thunderbolt.velocity = transform.right * speed;
    }

    void Update()
    {
        traveled += Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        if (traveled >= 20f || hitWall == true)
        {
            thunderbolt.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            anim.Play("Thunderbolt_Destroyed");
            Invoke("DestroyObject", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("takeDamage", damage);
        }

        else if (collision.gameObject.CompareTag("Wall"))
        {
            hitWall = true;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
