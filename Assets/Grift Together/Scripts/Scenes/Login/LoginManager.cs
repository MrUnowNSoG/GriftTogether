using UnityEngine;

namespace GriftTogether {
    public class LoginManager : BaseSceneManager {

        private Canvas _overlayCanvas;
        private LoginPresenter _presenter;
        private LoginRegisterPresenter _registerPresenter;

        public LoginManager(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public override void Init() {
            InitMVP();
        }

        private void InitMVP() {
            _presenter = new LoginPresenter(_overlayCanvas);
            _presenter.Initialize();

            _registerPresenter = new LoginRegisterPresenter(_overlayCanvas);
            _registerPresenter.Initialize();
        }

        public override void Deinitialize() {
            _registerPresenter.Deinitialize();
            _presenter.Deinitialize();
        }
    }
}
