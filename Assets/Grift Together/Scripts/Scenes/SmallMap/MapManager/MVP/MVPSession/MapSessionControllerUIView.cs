using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapSessionControllerUIView : MonoBehaviour, IView {

        [SerializeField] private TextMeshProUGUI _turnText;
        [SerializeField] private Button _turnButton;

        private MapSessionControllerPresenter _presenter;

        public event Action OnClose;

        
        public void Initialize(IPresenter presenter) {
            _presenter = (MapSessionControllerPresenter)presenter;
            UpdateTurnMessage(string.Empty);
            _turnButton.onClick.AddListener(TurnButton);
        }

        public void TurnButton() => _presenter.TurnButton();

        public void UpdateTurnMessage(string message) => _turnText.text = message;

        public void ShowUI() {
            gameObject.SetActive(true);
        }

        public void CloseUI() {
            gameObject.SetActive(false);
        }

        public void Deinitialize() {
            _turnButton.onClick.RemoveListener(TurnButton);
        }
    }
}
