using System;
using UnityEngine;

namespace GriftTogether {

    public class LoginRegisterView : MonoBehaviour, IView {

        [SerializeField] private LoginRegisterViewController _controller;

        private LoginRegisterPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LoginRegisterPresenter)presenter;
            InitController();
        }

        private void InitController() {
            _controller.OnLogin += TryLogin;
            _controller.OnRegister += TryRegister;

            _controller.Init();
        }

        private void TryLogin(LoginRegisterData data) {

        }

        private void TryRegister(LoginRegisterData data) {

        }

        public void Deinitialize() {
            _controller.OnLogin -= TryLogin;
            _controller.OnRegister -= TryRegister;

            _controller.Deinitialize();
        }


        public void HideUI() {}

        public void ShowUI() {}
    }
}
