using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderGenerator : MonoBehaviour
{
    public float timeToGenerate = 0.0f;
    public GameObject thisBoulder;
    //private Boulder boulder;
    //public Transform target;

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
            Debug.Log("Creating boulder");
            Instantiate(thisBoulder, transform.position, Quaternion.identity);
            //thisBoulder = GameObject.Find("Boulder");
            //boulder = thisBoulder.GetComponent<Boulder>();
            //boulder.destination = target;
            timeToGenerate = 0f;
        }
    }
}
