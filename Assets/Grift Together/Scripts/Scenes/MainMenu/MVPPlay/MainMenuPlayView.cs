using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuPlayView : MonoBehaviour, IView {

        [Space(0)][Header("Create lobby")]
        [SerializeField] private Button _createLobbyButton;

        [Space(10)][Header("Connect UI")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _connectButton;
        [SerializeField] private TMP_Text _errorText;

        [Space(10)][Header("Back lobby")]
        [SerializeField] private Button _backButton;


        private MainMenuPlayPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MainMenuPlayPresenter)presenter;
            InitUI();
        }

        private void InitUI() {
            _createLobbyButton.onClick.AddListener(CreateButton);

            _connectButton.onClick.AddListener(ConnectToLobby);
            UpdateErrorText(string.Empty);

            _backButton.onClick.AddListener(CloseUI);
        }

        private void CreateButton() => _presenter.CreateLobby();
        
        private void ConnectToLobby() {
            string message = _presenter.ConnectToLobby(_inputField.text);
            UpdateErrorText(message);
        }
        
        private void UpdateErrorText(string message) {
            if (string.IsNullOrEmpty(message)) return;
            _errorText.text = GameRoot.LocalizationManager.Get(message);
        }

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() {
            OnClose?.Invoke();
            gameObject.SetActive(false);
        }

        public void Deinitialize() {
            _backButton.onClick.AddListener(CloseUI);

            UpdateErrorText(string.Empty);
            _connectButton.onClick.RemoveListener(ConnectToLobby);

            _createLobbyButton.onClick.RemoveListener(CreateButton);
        }

    }
}
