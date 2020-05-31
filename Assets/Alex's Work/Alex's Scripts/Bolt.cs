using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    //Variables for bolt
    public float speed = 5f;
    public int boltDamage = 20;
    public Rigidbody2D leBolt;

    public float traveled = 0f;
    Vector2 lastPosition;
    private bool hitWall = false;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        leBolt.velocity = transform.right * speed;
    }

    void Update()
    {
        traveled += Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (traveled >= 10f)
        {
            DestroyBolt();
        }
    }

    //If bolt hits enemy, deal damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("takeDamage", boltDamage);
            DestroyBolt(); //Remove bolt after it hits enemy
        }

        else if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyBolt();
        }
    }

    //Destroy the bolt
    void DestroyBolt()
    {
        Destroy(gameObject);
    }
}
