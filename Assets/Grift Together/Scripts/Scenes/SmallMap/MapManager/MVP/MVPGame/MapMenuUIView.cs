using Photon.Pun;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {
    public class MapMenuUIView : MonoBehaviour, IView {

        [SerializeField] private Button _openMenuButton;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private GameObject[] _allMenuGO;

        [Space(10)][Header("Main Logic")]
        [SerializeField] private Button _leaveGameButton;

        private MapMenuPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapMenuPresenter)presenter;
            InitUI();
        }

        private void InitUI() {
            _leaveGameButton.onClick.AddListener(LeaveGame);
            _closeMenuButton.onClick.AddListener(CloseUI);
            _openMenuButton.onClick.AddListener(ShowUI);
        }

        private void LeaveGame() => _presenter.LeaveGame();

        public void ShowUI() => UpdateUI(true);
        public void CloseUI() => UpdateUI(false);

        private void UpdateUI(bool state) {
            foreach (var item in _allMenuGO) {
                item.gameObject.SetActive(state);
            }

            _openMenuButton.gameObject.SetActive(!state);
        }

        public void Deinitialize() {
            _openMenuButton.onClick.RemoveListener(ShowUI);
            _closeMenuButton.onClick.RemoveListener(CloseUI);
            _leaveGameButton.onClick.RemoveListener(LeaveGame);
        }
    }
}
