using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MultiPlayerUIController : PlayerUIController
    {
        [SerializeField] private Text _scoreText, _bestScoreText;
        [SerializeField] private Image[] _livesImages;

        public override void UpdateLives(Sprite livesSprite, int playerNumber)
        {
            _livesImages[playerNumber].sprite = livesSprite;
        }

        public override void UpdateScore(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public override void UpdateBestScore(int bestScore)
        {
            _bestScoreText.text = "Best: " + bestScore;
        }
    }
}
