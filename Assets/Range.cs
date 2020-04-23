using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public Transform firePoint;
    public GameObject boltPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
    }
}
