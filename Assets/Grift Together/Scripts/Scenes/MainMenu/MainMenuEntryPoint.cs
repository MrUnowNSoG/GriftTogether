using UnityEngine;

namespace GriftTogether {

    public class MainMenuEntryPoint : BaseEntryPoint {

        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _overlayCanvas;
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

            if(GameRoot.ServiceLocator.Resolve(out SkinPhotonService service) == false) {
                service = new SkinPhotonService();
                GameRoot.ServiceLocator.AddService(service);
            }

            if (GameRoot.ServiceLocator.Resolve(out TextValidatorService textValidatorService) == false) {
                TextValidatorService textValidatorService1 = new TextValidatorService();
                GameRoot.ServiceLocator.AddService(textValidatorService1);
            }
        }

        protected override void RegisterSceneServices() {
            if (_localServiceLocator.Resolve(out SkinService skinService) == false) {
                skinService = new SkinService();
                _localServiceLocator.AddService(skinService);
            }

            skinService.SetSkinAgent(_skinServiceAgent);
            skinService.ResolveCurrentSkin();
        }

        protected override void InitSceneManager() {
            _menuManager = new MainMenuManager(_mainCanvas, _overlayCanvas, _localServiceLocator);
            _menuManager.Initialize();
        }

        public override void Deinitialize() {
            _menuManager.Deinitialize();
        }
    }
}
