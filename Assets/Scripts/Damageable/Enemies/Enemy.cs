using System.Collections;
using Managers;
using PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speed = 4f;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private float _fireRate = 2f;

        private Player _player;
        private Animator _animator;
        private BoxCollider2D _collider;
        private AudioSource _audioSource;
        private Health _health;

        private GameManager _gameManager;

        private bool _isAlive = true;
        private float _nexFireTime;
    
        private static readonly int OnEnemyDeathTrigger = Animator.StringToHash("OnEnemyDeath");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator is NULL.");
            }

            _collider = GetComponent<BoxCollider2D>();
            if (_collider == null)
            {
                Debug.LogError("BoxCollider2D is NULL.");
            }

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("Audio Source on Enemy is NULL.");
            }

            _health = GetComponent<Health>();
            if (_health == null)
            {
                Debug.LogError("Health on enemy is NULL.");
            }

            _health.OnLivesChange += OnLivesChange;
                        
            _gameManager = ManagersAggregator.Get<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
            
            if (transform.position.y <= -6f && _isAlive)
            {
                var randomX = Random.Range(-9f, 9f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
            
            if (_isAlive &&  Time.time >= _nexFireTime)
            {
                ShootLaser();
            }
        }
        
        private void ShootLaser()
        {
            var playerLayerMask = LayerMask.GetMask("Player");
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down,  Mathf.Infinity, playerLayerMask);
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.transform.CompareTag("Player")) 
                {
                    _nexFireTime = Time.time + _fireRate;
                    Instantiate(_laserPrefab, transform.position, Quaternion.identity); 
                }
            }
        }
        
        private void OnLivesChange(int lives)
        {
            if (lives < 1)
            {
                OnEnemyDestroy();
            } 
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                OnPlayerEnter(other);
            }
        }

        void OnPlayerEnter(Collider2D other)
        {
            var playerHealth = other.transform.GetComponent<Health>();
            
            if (playerHealth != null)
            {
                playerHealth.Damage();
            }
        
            OnEnemyDestroy();
        }

        private void OnEnemyDestroy()
        {
            _collider.enabled = false;
            _isAlive = false;
            _animator.SetTrigger(OnEnemyDeathTrigger);
            _gameManager.AddScore(10);

            StartCoroutine(StopEnemyRoutine());
        
            Destroy(this.gameObject, 2.35f);
        }

        IEnumerator StopEnemyRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            _speed = 0f;
            _audioSource.Play();
        }
    }
}