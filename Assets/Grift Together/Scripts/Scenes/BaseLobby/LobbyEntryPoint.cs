using UnityEngine;

namespace GriftTogether {

    public class LobbyEntryPoint : BaseEntryPoint {

        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _overlayCanvas;

        private LobbyManager _manager;

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

            if(GameRoot.ServiceLocator.Resolve(out DustService dustService) == false) {
                dustService = new DustService();
                GameRoot.ServiceLocator.AddService(dustService);

            } else dustService.SetDefaultDust();
        }
        protected override void RegisterSceneServices() {}

        protected override void InitSceneManager() {
            _manager = new LobbyManager(_mainCanvas, _overlayCanvas, _localServiceLocator);
            _manager.Init();

            LobbyPhotonRPCContext context = new LobbyPhotonRPCContext();
            context.LobbyManager = _manager;
            GameRoot.PhotonManager.CurrentPhotonContext = context;
        }

        public override void Deinitialize() {
            _manager.Deinitialize();
        }
    }
}
