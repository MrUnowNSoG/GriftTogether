using UnityEngine;

namespace GriftTogether {

    public class MainMenuEntryPoint : BaseEntryPoint {

        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _overlayCanvas;
        [SerializeField] protected SkinServiceAgent _skinServiceAgent;

        private MainMenuManager _menuManager;

        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
            RegisterGameServices();
            RegisterSceneServices();
            InitSceneManager();
        }

        protected override void RegisterGameServices() {

            if(GameRoot.ServiceLocator.Resolve(out SkinService skinService)) {

            } else {
                skinService = new SkinService();
                GameRoot.ServiceLocator.AddService(skinService);
            }

            skinService.SetSkinAgent(_skinServiceAgent);
            skinService.ResolveCurrentSkin();
        }
        protected override void RegisterSceneServices() {}

        protected override void InitSceneManager() {
            _menuManager = new MainMenuManager(_mainCanvas, _overlayCanvas);
            _menuManager.Init();
        }

        protected override void Deinitialize() {}
    }
}
