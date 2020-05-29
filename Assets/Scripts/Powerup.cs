using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    [SerializeField] private int _powerupID; //0 - triple shot, 1 - speed, 2 - shield

    [SerializeField] private AudioClip _powerupAudioClip;
    
    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        if (transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var powerupsController = other.transform.GetComponent<PowerupsController>();

            if (powerupsController != null)
            {
                AudioSource.PlayClipAtPoint(_powerupAudioClip, new Vector3(0 ,0, -5));
                
                //TODO Использовать полиморфизм?
                switch (_powerupID)
                {
                    case 0: 
                        powerupsController.ActivateTripleShot();
                        break;
                    case 1:
                        powerupsController.ActivateSpeedBoost();
                        break;
                    case 2:
                        powerupsController.ActivateShield();
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }
}
