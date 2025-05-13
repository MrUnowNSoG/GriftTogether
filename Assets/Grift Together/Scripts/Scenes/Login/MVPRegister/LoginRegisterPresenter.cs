using UnityEngine;

namespace GriftTogether {

    public class LoginRegisterPresenter : IPresenter {

        private LoginRegisterModel _model;
        private LoginRegisterView _view;

        private Canvas _overlayCanvas;
        private AuthService _authService;

        public LoginRegisterPresenter(Canvas canvas) {
            _overlayCanvas = canvas;
            _authService = new AuthService(GameRoot.FireStoreManager);
        }

        public void Initialize() {
            _model = new LoginRegisterModel();
            _view = GameRoot.PrefabManager.InstantiatePrefab(LoginPrefabType.LoginRegisterView, _overlayCanvas.gameObject).GetComponent<LoginRegisterView>();
            _view.Initialize(this);

            _authService.OnError += ErrorAuth;
        }

        public async void TryLogin(LoginRegisterData data) {
            bool result = await _authService.LoginAsync(data.Login, data.Password);
            IsSuccessful(result);
        }

        public async void TryRegister(LoginRegisterData data) {
            bool result = await _authService.RegisterAsync(data.Login, data.Password, data.UserName);
            IsSuccessful(result);
        }

        private void ErrorAuth(string message) {
            _view.SetErrorText(message);
        }

        private void IsSuccessful(bool state) {

            if (state) {

                GameRoot.ScenesManager.ShowLoadingScreen();

                CloseUI();
                Deinitialize();
                GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.MENU_SCENE, true);
            }
        }

        public void ShowUI() { }
        public void CloseUI() {}

        public void Deinitialize() {
            _authService.OnError -= ErrorAuth;
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
