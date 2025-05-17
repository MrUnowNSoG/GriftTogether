using UnityEngine;

namespace GriftTogether {

    public class MapEntryPoint : BaseEntryPoint {

        [Space(0)][Header("Root Object")]
        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _overlayCanvas;

        [Space(10)][Header("Manage")]
        [SerializeField] private MapPhotonManager _photonManager;
        [SerializeField] private MapPhotonRPCManager _rpcManager;
        [SerializeField] private PlaygroundManager _playgroundManager;
        [SerializeField] private MapUIManager _mapUIManager;
        [SerializeField] private MapGameManager _mapGameManager;

        [Space(10)][Header("Service")]
        [SerializeField] private MapPhotonTurnService _turnService;

        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
            RegisterGameServices();
            RegisterSceneServices();
            InitSceneManager();
        }

        protected override void RegisterGameServices() {}
        protected override void RegisterSceneServices() {
        
            if(_localServiceLocator.Resolve(out MapPhotonTurnService turnService) == false) {
                _localServiceLocator.AddService(_turnService);
            }
            _turnService.Init(_localServiceLocator);

        }

        protected override void InitSceneManager() {
            _mapUIManager = new MapUIManager(_mainCanvas);
            _mapGameManager = new MapGameManager();

            _rpcManager = new MapPhotonRPCManager(_localServiceLocator, _mapUIManager, _mapGameManager, _overlayCanvas);

            _playgroundManager.Initialize();

            _photonManager.Initialize(_localServiceLocator, _playgroundManager);

            _mapUIManager.Initialize();
            _mapGameManager.Initialize();

            _rpcManager.Initialize();

            _photonManager.PlayerReady();
        }


        public override void Deinitialize() {

        }

    }
}
