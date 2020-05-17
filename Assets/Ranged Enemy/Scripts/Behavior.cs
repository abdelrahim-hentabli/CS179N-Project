using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    public GameObject leftBound;
    public GameObject rightBound;

    public GameObject player;

    private Rigidbody2D body;

    public Animator animator;

    public float speed;

    const float WALK_SPEED = .5f;

    const float BOLT_COOLDOWN = .2f;

    bool right = true;

    float pauseTimer;
    float boltTimer;

    bool waiting = false;

    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        pauseTimer = 0;
        boltTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= 1.0f)
            {
                animator.SetTrigger("Move");
                pauseTimer = 0;
                speed = WALK_SPEED;
                right = !right;
                Vector3 localScale = transform.localScale;
                localScale.x = right ? 1 : -1;
                transform.localScale = localScale;
                waiting = false;
            }
        }
        else if ((right && transform.position.x >= rightBound.transform.position.x) || (!right && transform.position.x <= leftBound.transform.position.x))
        {
            waiting = true;
            animator.SetTrigger("Wait");
            speed = 0;
        }

        body.velocity = new Vector2( (right?1:-1) * speed, body.velocity.y);
    }
}
