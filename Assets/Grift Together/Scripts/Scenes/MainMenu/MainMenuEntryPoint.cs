using UnityEngine;

namespace GriftTogether {

    public class MainMenuEntryPoint : BaseEntryPoint {

        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _overlayCanvas;


        private MainMenuManager _menuManager;

        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
            RegisterSceneServices();
            InitSceneManager();
        }

        protected override void RegisterSceneServices() {}

        protected override void InitSceneManager() {
            _menuManager = new MainMenuManager(_mainCanvas, _overlayCanvas);
            _menuManager.Init();
        }
    }
}
