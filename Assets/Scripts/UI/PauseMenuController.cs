using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private Button _resumeBtn, _mainMenuBtn;

        public event Action OnPauseButtonClicked = delegate { }, OnMainMenuButtonClicked = delegate {  };

        // Start is called before the first frame update
        void Start()
        {
            _resumeBtn.onClick.AddListener(delegate { OnPauseButtonClicked(); });
            _mainMenuBtn.onClick.AddListener(delegate { OnMainMenuButtonClicked(); });
        }
    
    }
}
