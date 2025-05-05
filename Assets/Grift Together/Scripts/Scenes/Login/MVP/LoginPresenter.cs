using UnityEngine;

namespace GriftTogether {

    public class LoginPresenter : IPresenter {

        private Canvas _overlayCanvas;

        private LoginView _view;

        public LoginPresenter(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LoginPrefabType.LoginView, _overlayCanvas.gameObject).GetComponent<LoginView>();
            _view.Initialize(this);
            _view.ShowUI();
        }

        public void Deinitialize() {
            throw new System.NotImplementedException();
        }

        public void CloseUI() {
            throw new System.NotImplementedException();
        }
    }
}
