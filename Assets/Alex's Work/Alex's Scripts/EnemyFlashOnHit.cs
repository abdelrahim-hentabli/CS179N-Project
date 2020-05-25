using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashOnHit : MonoBehaviour
{
    private Material flash;
    private Material thisDefault;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        flash = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        thisDefault = sr.material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bolt") || collision.CompareTag("Player"))
        {
            sr.material = flash;
            Invoke("ResetMaterial", 0.1f);
        }
    }
    void ResetMaterial()
    {
        sr.material = thisDefault;
    }
}
