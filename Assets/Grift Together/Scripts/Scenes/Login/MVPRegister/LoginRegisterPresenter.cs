using UnityEngine;

namespace GriftTogether {

    public class LoginRegisterPresenter : IPresenter {

        private Canvas _overlayCanvas;

        private LoginRegisterModel _model;
        private LoginRegisterView _view;

        public LoginRegisterPresenter(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public void Initialize() {
            _model = new LoginRegisterModel();
            _view = GameRoot.PrefabManager.InstantiatePrefab(LoginPrefabType.LoginRegisterView, _overlayCanvas.gameObject).GetComponent<LoginRegisterView>();
            _view.Initialize(this);
        }



        public void CloseUI() {}

        public void Deinitialize() {}

    }
}
