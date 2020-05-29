using Managers;
using UI;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _playerNumber = 0;
        [SerializeField] private float _fireRate = 0.2f;

        [Space]
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private GameObject _tripleShotPrefab;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject[] _damagePrefabs;

        [SerializeField] private AudioClip _laserAudioClip;
    
        private bool _isTripleShotActive = false;
        private float _nextFireTime = 0.0f;

        private UIManager _uiManger;
        private SpawnManager _spawnManager;
        private GameManager _gameManager;
        private AudioSource _audioSource;

        private PlayerHealth _health;
        private PowerupsController _powerupsController;

        public int PlayerNumber
        {
            get => _playerNumber;
            private set => _playerNumber = value;
        }

        // Start is called before the first frame update
        void Start()
        {
            _spawnManager = ManagersAggregator.Get<SpawnManager>();
            _uiManger = ManagersAggregator.Get<UIManager>();
            _gameManager = ManagersAggregator.Get<GameManager>();

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("Audio Source on Player is NULL.");
            }
            else
            {
                _audioSource.clip = _laserAudioClip;
            }

            _health = GetComponent<PlayerHealth>();
            _health.OnLivesChange += UpdateLives;

            _powerupsController = GetComponent<PowerupsController>();
            _powerupsController.TripleShotStateChanges += (isActive) => { _isTripleShotActive = isActive; };
        }

        // Update is called once per frame
        void Update()
        {
            if (CanFire())
            {
                ShootLaser();
            }
        }

        private bool CanFire()
        {
            return Time.time > _nextFireTime;
        }
    

        private void ShootLaser()
        {
            _nextFireTime = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Vector3 laserOffset = new Vector3(0, 1.0f, 0);
                Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
            }

            _audioSource.Play();
        }

        private void UpdateLives(int lives)
        {
            _uiManger.UpdateLives(Mathf.Clamp(lives, 0, 3), _playerNumber);
        
            if (lives == 2)
            {
                ActivateFirstDamage();
            }
            else if (lives == 1)
            {
                ActivateSecondDamage();
            }
            else if (lives == 0)
            {
                _spawnManager.OnPlayerDeath();
                _gameManager.GameOver();
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                Destroy(this.gameObject);
            }
        }

        private void ActivateFirstDamage()
        {
            var damageId = Random.Range(0, _damagePrefabs.Length);
            _damagePrefabs[damageId].SetActive(true);
        }

        private void ActivateSecondDamage()
        {
            foreach (var damage in _damagePrefabs)
            {
                if (damage.activeSelf == false)
                {
                    damage.SetActive(true);
                }
            }
        }
    }
}