using System;
using System.Collections;
using UnityEngine;

namespace GriftTogether {

    public class LoginRegisterView : MonoBehaviour, IView {

        private const float FULL_OPACITY = 1f;
        private const float ZERO_OPACITY = 0f;
        private const float TIME_FADE_LOADIN_SCREEN = 1.2f;

        [SerializeField] private LoginRegisterViewController _controller;
        [SerializeField] private CanvasGroup _loadingScreen;

        private LoginRegisterPresenter _presenter;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _presenter = (LoginRegisterPresenter)presenter;

            SetLoadingScreen(false, FULL_OPACITY);
            
            InitController();
        }

        private void InitController() {
            _controller.OnLogin += TryLogin;
            _controller.OnRegister += TryRegister;

            _controller.Init();
        }

        private void TryLogin(LoginRegisterData data) {
            SetLoadingScreen(true, FULL_OPACITY);
            _presenter.TryLogin(data);
        }

        private void TryRegister(LoginRegisterData data) {
            SetLoadingScreen(true, FULL_OPACITY);
            _presenter.TryRegister(data);
        }

        public void SetErrorText(string str) {
            _controller.SetErrorText(str);
            GameRoot.CoroutinsManager.FadeAnimation(_loadingScreen, FULL_OPACITY, ZERO_OPACITY, TIME_FADE_LOADIN_SCREEN, false);
        }

        public void Deinitialize() {
            _controller.OnLogin -= TryLogin;
            _controller.OnRegister -= TryRegister;

            _controller.Deinitialize();
        }

        private void SetLoadingScreen(bool isActive, float alpha) {
            _loadingScreen.alpha += alpha;
            _loadingScreen.gameObject.SetActive(isActive);
        }

        public void CloseUI() {}

        public void ShowUI() {}
    }
}
