using UnityEngine;

namespace UI
{
    public abstract class PlayerUIController : MonoBehaviour
    {

        public abstract void UpdateLives(Sprite livesSprite, int playerNumber);

        public abstract void UpdateScore(int score);
    
        public abstract void UpdateBestScore(int bestScore);

    }
}
