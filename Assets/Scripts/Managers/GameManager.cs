using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Manager
    {
        [SerializeField] private string _gameMode;
        [SerializeField] private bool _isGameOver;
        [SerializeField] private bool _isPause;

        [SerializeField] private int _score;
        [SerializeField] private int _bestScore;

        [Space] 
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _twoPlayersPrefab;
        [SerializeField] private GameObject _asteroidPrefab;

        private UIManager _uiManager;

        private const string SinglePlayerBestScoreKey = "single-player-best-score-key";
        private const string MultiPlayerBestScoreKey = "multi-player-best-score-key";

        public string GameMode
        {
            get => _gameMode;
            private set => _gameMode = value;
        }

        private void Start()
        {
            _uiManager = ManagersAggregator.Get<UIManager>();

            SetPauseActive(false);
            _isGameOver = false;

            _bestScore = PlayerPrefs.GetInt(SinglePlayerBestScoreKey, 0);
            _uiManager.UpdateBestScore( _bestScore );
        
            _uiManager.OnPausing += ResumeGame;
            _uiManager.OnGoingTiMainMenu += GoToMainMenu;
            
            InitGameMode();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForRestart();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPauseActive(!_isPause);
            }
        }

        public void GameOver()
        {
            _isGameOver = true;

            UpdateBestScore();
        }
        

        public void AddScore(int newScore)
        {
            if (_isGameOver == false)
            {
                _score += newScore;
                _uiManager.UpdateScore(_score);
            }
        }

        private void InitGameMode()
        {
            _gameMode = PlayerPrefs.GetString(GameConstants.GameMode);
            
            if (_gameMode.Equals(GameConstants.SinglePlayer))
            {
                Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
                Instantiate(_asteroidPrefab, new Vector3(-5, 3, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_twoPlayersPrefab, Vector3.zero, Quaternion.identity);
                Instantiate(_asteroidPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            }
        }
        
        private void UpdateBestScore()
        {
            if (_score > _bestScore)
            {
                _bestScore = _score;
                _uiManager.UpdateBestScore(_bestScore);
                if (_gameMode.Equals(GameConstants.SinglePlayer))
                {
                    PlayerPrefs.SetInt(SinglePlayerBestScoreKey, _bestScore);
                }
                else
                {
                    PlayerPrefs.SetInt(MultiPlayerBestScoreKey, _bestScore);
                }
            }
        }
    
        private void CheckForRestart()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
        }

        private void SetPauseActive(bool isPauseActive)
        {
            _isPause = isPauseActive;
            _uiManager.SetPauseMenuActive(isPauseActive);
            Time.timeScale = isPauseActive ? 0 : 1;
        }

        public void ResumeGame()
        {
            SetPauseActive(false);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0); //Main Menu scene
        }
    }
}
