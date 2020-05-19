using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemy : MonoBehaviour
{
    public bool seeEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        seeEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       seeEnemy = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       seeEnemy = false;
    }
}
