using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapPopUpLostView : MonoBehaviour, IView {

        [SerializeField] private Button _leaveButton;
        [SerializeField] private Button _stayButton;

        private MapPopUpPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {

            _presenter = (MapPopUpPresenter)presenter;

            _stayButton.onClick.AddListener(StayGame);
            _leaveButton.onClick.AddListener(LeaveGame);

            CloseUI();
        }

        private void LeaveGame() => _presenter.LeaveGame();

        private void StayGame() => _presenter.StayGame();

        public void ShowUI() => gameObject.SetActive(true);
        public void CloseUI() => gameObject.SetActive(false);

        public void Deinitialize() {
            _leaveButton.onClick.RemoveListener(LeaveGame);
            _stayButton.onClick.RemoveListener(StayGame);
        }
    }
}
