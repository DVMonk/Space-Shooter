using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class UIManager : Manager
    {
        [SerializeField] private Text _scoreText, _bestScoreText;

        [SerializeField] private Image _livesImage;
    
        [SerializeField] private Sprite[] _livesSprites;

        [SerializeField] private Text _gameOverText;
        [SerializeField] private Text _restartText;

        [Space]
        [SerializeField] private GameObject _singlePlayerPanel;
        [SerializeField] private GameObject _multiPlayerPanel;
        [SerializeField] private GameObject _pauseMenuPanel;

        private string _gameMode;
        private PauseMenuController _pauseMenuController;
        private Animator _pauseMenuAnimator;
        private PlayerUIController _playerUiController;

        private static readonly int IsPause = Animator.StringToHash("isPause");

        public event Action OnPausing = delegate {  }, OnGoingTiMainMenu = delegate {  };

        // Start is called before the first frame update
        void Start()
        {
            _pauseMenuAnimator = _pauseMenuPanel.GetComponent<Animator>();
            _pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        
            _gameOverText.gameObject.SetActive(false);
            _restartText.gameObject.SetActive(false);

            _pauseMenuController = _pauseMenuPanel.GetComponent<PauseMenuController>();
            _pauseMenuController.OnPauseButtonClicked += delegate { OnPausing(); };
            _pauseMenuController.OnMainMenuButtonClicked += delegate { OnGoingTiMainMenu(); };

            InitGameMode();
            UpdateScore(0);
            UpdateLives(3);
        }

        public void InitGameMode()
        {
            _gameMode = PlayerPrefs.GetString(GameConstants.GameMode);
            if (_gameMode.Equals(GameConstants.SinglePlayer))
            {
                _singlePlayerPanel.SetActive(true);
                _multiPlayerPanel.SetActive(false);

                _playerUiController = _singlePlayerPanel.GetComponent<PlayerUIController>();
            }
            else
            {
                _singlePlayerPanel.SetActive(false);
                _multiPlayerPanel.SetActive(true);
                
                _playerUiController = _multiPlayerPanel.GetComponent<PlayerUIController>();
            }
        }

        public void SetPauseMenuActive(bool isActive)
        {
            _pauseMenuPanel.SetActive(isActive);
            _pauseMenuAnimator.SetBool(IsPause, isActive);
        }
    
        public void UpdateScore(int score)
        {
            _playerUiController.UpdateScore(score);
        }

        public void UpdateBestScore(int newBest)
        {
            _bestScoreText.text = "Best: " + newBest;
        }

        public void UpdateLives(int playerLives, int playerNumber = 0)
        {
            _playerUiController.UpdateLives(_livesSprites[playerLives], playerNumber);
            
            if (playerLives == 0)
            {
                StartCoroutine(GameOverFlickerRoutine());
                _restartText.gameObject.SetActive(true);
            }
        }

        IEnumerator GameOverFlickerRoutine()
        {
            _gameOverText.gameObject.SetActive(true);
            while (true)
            {
                _gameOverText.text = "GAME OVER";
                yield return new WaitForSeconds(0.5f);
            
                _gameOverText.text = "";
                yield return new WaitForSeconds(0.5f);
            }
        }
    
    }
}
