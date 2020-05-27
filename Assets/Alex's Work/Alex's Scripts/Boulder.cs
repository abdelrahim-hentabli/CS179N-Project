using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public Rigidbody2D boulder;
    public Animator anim;
    public CircleCollider2D trigger;
    public CircleCollider2D boulderCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boulder hit player");
            Destroy(trigger);
            Destroy(boulderCollider);
            anim.SetTrigger("HitPlayer");
            Invoke("DestroyBoulder", 0.80f);
        }
    }

    void DestroyBoulder()
    {
        Destroy(gameObject);
    }
}
