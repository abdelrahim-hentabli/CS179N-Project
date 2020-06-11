using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    const float TIME_OUT_TIME = 5f;
    float timeOut;
    // Start is called before the first frame update
    void Start()
    {
        timeOut = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeOut += Time.deltaTime;
        if(timeOut >= TIME_OUT_TIME)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
