using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LoginView : MonoBehaviour, IView {

        [Space(0)] [Header("Buttons")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        [Space(10)] [Header("Info Panel")]
        [SerializeField] private TextMeshProUGUI _logText;

        private LoginPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LoginPresenter)presenter;
            ButtonInit();   
        }

        private void ButtonInit() {

        }

        public void ShowUI() {

        }

        public void HideUI() {
            
        }


        public void Deinitialize() {
        }
    }
}
