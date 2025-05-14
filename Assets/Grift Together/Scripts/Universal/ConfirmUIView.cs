using System;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class ConfirmUIView : MonoBehaviour, IView {

        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _declineButton;

        public event Action OnConfirm;
        public event Action OnDecline;

        public event Action OnClose;


        public void Initialize(IPresenter presenter) {
            _confirmButton.onClick.AddListener(ConfirmButton);
            _declineButton.onClick.AddListener(DeclineButton);
        }

        private void ConfirmButton() => OnConfirm?.Invoke();
        private void DeclineButton() => OnDecline?.Invoke();

        public void ShowUI() => this.gameObject.SetActive(true);
        public void CloseUI() {
            OnClose?.Invoke();
            this.gameObject.SetActive(false);
        }

        public void Deinitialize() {
            _confirmButton.onClick.RemoveAllListeners();
            _declineButton.onClick.RemoveAllListeners();
        }

    }
}
