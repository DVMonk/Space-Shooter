using System.Collections;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 3.0f;

        [SerializeField] private GameObject _explosionPrefab;

        private SpawnManager _spawnManager;
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Health _health;

        // Start is called before the first frame update
        void Start()
        {
            _spawnManager = ManagersAggregator.Get<SpawnManager>();

            _collider = GetComponent<CircleCollider2D>();
            if (_collider == null)
            {
                Debug.LogError("Collider is Null.");
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                Debug.LogError("Sprite Renderer on Asteroid is NULL.");
            }

            _health = GetComponent<Health>();
            if (_health == null)
            {
                Debug.LogError("Health on Asteroid is NULL.");
            }

            _health.OnLivesChange += OnLivesChanged;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward * (_rotateSpeed * Time.deltaTime));
        }

        private void OnLivesChanged(int lives)
        {
            if (lives < 1)
            {
                _spawnManager.StartSpawning();
                _collider.enabled = false;
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.25f);
            }
        }
    }
}