using PlayerScripts;
using UnityEngine;

namespace Enemies
{
    public class EnemyLaser : MonoBehaviour
    {
        [SerializeField] private float _speed = 8.0f;


        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
            
            if (transform.position.y <= -8f)
            {
                var parent = transform.parent;
                if (parent != null)
                {
                    Destroy(parent.gameObject);
                }
            
                Destroy(this.gameObject);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<PlayerHealth>();
                playerHealth.Damage();

                if (transform.parent)
                {
                    Destroy(transform.parent.gameObject);
                }
                
                Destroy(this.gameObject);
            }
        }
    }
}
