using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LoginView : MonoBehaviour, IView {

        [Space(0)] [Header("Buttons")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _randomButton;
        [SerializeField] private Button _loadButton;

        [Space(10)] [Header("Info Panel")]
        [SerializeField] private TextMeshProUGUI _logText;

        private LoginPresenter _presenter;

        public event Action OnClose;
        public event Action OnSave;
        public event Action OnRandom;
        public event Action OnLoad;

        public void Initialize(IPresenter presenter) {
            _presenter = (LoginPresenter)presenter;
            ButtonInit();   
        }

        private void ButtonInit() {
            _saveButton.onClick.AddListener(SaveButton);
            _randomButton.onClick.AddListener(RandomButton);
            _loadButton.onClick.AddListener(LoadButton);
        }


        private void SaveButton() {
            _presenter.SavePlayer();
        }

        private void RandomButton() {
            _presenter.RandomPlayer();
        }
        private void LoadButton() {
            _presenter.LoadPlayer();
        }

        public void UpdateLog(string logText) {
            _logText.text += ("\n" + logText);
        }

        public void Deinitialize() {
            _saveButton.onClick.RemoveListener(SaveButton);
            _randomButton.onClick.RemoveListener(RandomButton);
            _loadButton.onClick.RemoveListener(LoadButton);
        }

        public void ShowUI() { }
        public void HideUI() { }
    }
}
