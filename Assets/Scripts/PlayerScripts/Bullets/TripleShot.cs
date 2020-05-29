using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
