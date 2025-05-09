using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuExitView : MonoBehaviour, IView { 

        [Space(10)][Header("Buttons")]
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _stayGameButton;

        public event Action OnClose;
        public event Action OnExit;

        public void Initialize(IPresenter presenter) {
            InitButtons();
            ShowUI();
        }

        private void InitButtons() {
            _exitGameButton.onClick.AddListener(Exit);
            _stayGameButton.onClick.AddListener(Stay);
        }

        private void Stay() {
            CloseUI();
        }

        private void Exit() {
            CloseUI();
            OnExit?.Invoke();
        }

        public void ShowUI() {
            gameObject.SetActive(true);
        }


        public void CloseUI() {
            gameObject.SetActive(false);
        }


        public void Deinitialize() {
        }
    }
}
