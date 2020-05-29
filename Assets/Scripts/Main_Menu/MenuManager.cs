using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class MenuManager : MonoBehaviour
    {
        
        public void LoadSinglePlayer()
        {
            PlayerPrefs.SetString(GameConstants.GameMode, GameConstants.SinglePlayer);
            SceneManager.LoadScene(1);
        }

        public void LoadMultiplayer()
        {
            PlayerPrefs.SetString(GameConstants.GameMode, GameConstants.MultiPlayer);
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        { 
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    
    }
}
