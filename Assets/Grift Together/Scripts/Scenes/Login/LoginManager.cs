using UnityEngine;

namespace GriftTogether {
    public class LoginManager : BaseSceneManager {

        private Canvas _overlayCanvas;
        private LoginPresenter _presenter;

        public LoginManager(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public override void Init() {
            InitMVP();
        }

        private void InitMVP() {
            _presenter = new LoginPresenter(_overlayCanvas);
            _presenter.Initialize();
        }

        public override void DeInit() {

        }
    }
}
