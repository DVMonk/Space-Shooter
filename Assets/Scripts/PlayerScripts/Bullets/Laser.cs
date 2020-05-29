using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));

        if (transform.position.y >= 8f)
        {
            var parent = gameObject.transform.parent;
            if (parent != null)
            {
                Destroy(parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.Damage();

                Destroy(this.gameObject);
            }
        }
    }
}