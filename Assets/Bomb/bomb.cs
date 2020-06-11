﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders() { return colliders; }
    private float timer = 0.0f;

    private const float explode_time = 6f;

    public int bombStrength = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > explode_time)
        {
            for(int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].gameObject.tag == "PlayerHitbox")
                {
                    colliders[i].gameObject.SendMessageUpwards("TakeDamage", bombStrength);
                }
                else if (colliders[i].gameObject.CompareTag("Enemy"))
                {
                    colliders[i].gameObject.SendMessage("takeDamage", bombStrength);
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
}
