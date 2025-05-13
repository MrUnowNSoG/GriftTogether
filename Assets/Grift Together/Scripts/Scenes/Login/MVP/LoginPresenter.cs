using UnityEngine;

namespace GriftTogether {

    public class LoginPresenter : IPresenter {

        private Canvas _overlayCanvas;

        private LoginView _view;

        public LoginPresenter(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LoginPrefabType.LoginBackgroundView, _overlayCanvas.gameObject).GetComponent<LoginView>();
            _view.Initialize(this);
            _view.ShowUI();
        }


        public void ShowUI() { }
        public void CloseUI() {}

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
