using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LoginView : MonoBehaviour, IView {

        private LoginPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LoginPresenter)presenter;
        }

        public void Deinitialize() {
        }

        public void ShowUI() { }
        public void CloseUI() { }
    }
}
