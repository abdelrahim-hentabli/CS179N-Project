using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderGenerator : MonoBehaviour
{
    public float timeToGenerate = 0.0f;
    public GameObject thisBoulder;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToGenerate += Time.deltaTime;

        if (timeToGenerate >= 5.0f)
        {
            Instantiate(thisBoulder, transform.position, Quaternion.identity);
            timeToGenerate = 0f;
        }
    }
}
