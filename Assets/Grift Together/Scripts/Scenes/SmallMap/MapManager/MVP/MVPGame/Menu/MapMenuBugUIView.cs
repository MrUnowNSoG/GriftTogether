using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class MapMenuBugUIView : MonoBehaviour, IView {

        [Space(0)][Header("Input")]
        [SerializeField] private TMP_InputField _headerInput;
        [SerializeField] private TMP_InputField _descriptionInput;

        [Space(10)][Header("Button")]
        [SerializeField] private Button _sendButton;
        [SerializeField] private Button _closeButton;

        private MapMenuPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (MapMenuPresenter)presenter;

            _closeButton.onClick.AddListener(CloseUI);
            _sendButton.onClick.AddListener(SentButton);

            CloseUI();
        }

        private void SentButton() {
            if (string.IsNullOrEmpty(_headerInput.text)) return;
            if(string.IsNullOrEmpty(_descriptionInput.text)) return;
        
            _presenter.SendBug(_headerInput.text, _descriptionInput.text);
            CloseUI();
        }

        public void ShowUI() {
            _headerInput.text = string.Empty;
            _descriptionInput.text = string.Empty;

            gameObject.SetActive(true);
        }

        public void CloseUI() {
            _headerInput.text = string.Empty;
            _descriptionInput.text = string.Empty;

            gameObject.SetActive(false);

        }

        public void Deinitialize() {
            _closeButton.onClick.RemoveListener(CloseUI);
            _sendButton.onClick.RemoveListener(SentButton);
        }
    }
}
