using Photon.Pun;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LobbyMenuUiView : MonoBehaviour, IView {

        [SerializeField] private Button _startLobbyButton;
        [SerializeField] private Button _changeDustButton;
        [SerializeField] private TextMeshProUGUI _currentDustText;
        [SerializeField] private Button _leaveLobbyButton;
        
        private LobbyMenuPresenter _presenter;
        private bool _isDustChanging = false;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LobbyMenuPresenter)presenter;
        
            InitButtons();
            _currentDustText.text = string.Empty;
            _isDustChanging = false;
        }

        private void InitButtons() {
            _startLobbyButton.onClick.AddListener(StartLobby);
            _changeDustButton.onClick.AddListener(ChangeDust);
            _leaveLobbyButton.onClick.AddListener(LeaveLobby);
        }

        private void StartLobby() => _presenter.StartLobby();
        private void ChangeDust() {
            if (_isDustChanging == false) {
                _isDustChanging = true;
                _presenter.ChangeDust();
            }
        }
        private void LeaveLobby() => _presenter.LeaveLobby();


        public void ShowUI() {

            if (PhotonNetwork.IsMasterClient) {
                _startLobbyButton.gameObject.SetActive(true);
                _changeDustButton.gameObject.SetActive(true);
            }

            _leaveLobbyButton.gameObject.SetActive(true);

            this.gameObject.SetActive(true);
        }

        public void ShowCurrentDust(string dust) {
            _currentDustText.text = dust;
            _isDustChanging = false;
        }

        public void CloseUI() { 
            
            OnClose?.Invoke();
            this.gameObject.SetActive(false);

            _startLobbyButton.gameObject.SetActive(false);
            _changeDustButton.gameObject.SetActive(false);
            _leaveLobbyButton.gameObject.SetActive(false);
        }


        public void Deinitialize() {

            _startLobbyButton.onClick.RemoveListener(StartLobby);
            _changeDustButton.onClick.RemoveListener(ChangeDust);
            _leaveLobbyButton.onClick.RemoveListener(LeaveLobby);

        }

    }
}
