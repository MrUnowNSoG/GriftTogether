using UnityEngine;

namespace GriftTogether {

    public class LoginEntryPoint : BaseEntryPoint {

        [SerializeField] private Canvas _overlayCanvas;
        [SerializeField] private Canvas _cameraCanvas;

        private LoginManager _loginManager;


        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
            RegisterSceneServices();
            InitSceneManager();
        }

        protected override void RegisterSceneServices() {
     
        }

        protected override void InitSceneManager() {
            _loginManager = new LoginManager(_overlayCanvas);
            _loginManager.Init();
        }

    }
}
