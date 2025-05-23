using UnityEngine;

namespace GriftTogether {

    public class MapEntryPoint : BaseEntryPoint {

        [Space(0)][Header("Root Object")]
        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _overlayCanvas;

        [Space(10)][Header("Manage")]
        [SerializeField] private MapPhotonManager _photonManager;
        [SerializeField] private PlaygroundManager _playgroundManager;
        private MapPhotonRPCManager _rpcManager;
        private MapManager _mapManager;

        [Space(10)][Header("Service")]
        [SerializeField] private MapPhotonTurnService _turnService;
        [SerializeField] private MapSwitchCameraService _switchCameraService;

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
        
            if(_localServiceLocator.Resolve(out AnalyticsService service) == false) {
                service = new AnalyticsService(GameRoot.FireStoreManager);
                _localServiceLocator.AddService(service);
            }

            if(_localServiceLocator.Resolve(out MapPhotonTurnService turnService) == false) {
                _localServiceLocator.AddService(_turnService);
            }
            _turnService.Init(_localServiceLocator);

            if(_localServiceLocator.Resolve(out MapSwitchCameraService switchCameraService) == false) {
                _localServiceLocator.AddService(_switchCameraService);
            }
            _switchCameraService.Initialize();

        }

        protected override void InitSceneManager() {

            _mapManager = new MapManager(_mainCanvas, _overlayCanvas, _playgroundManager, _photonManager, _localServiceLocator);
            _rpcManager = new MapPhotonRPCManager(_localServiceLocator, _mapManager, _playgroundManager, _overlayCanvas);

            MapPlayerObject player = _photonManager.Initialize(_localServiceLocator, _playgroundManager, _mapManager);
            
            _playgroundManager.Initialize(_mapManager, player, _localServiceLocator);

            _mapManager.Initialize(player);

            _rpcManager.Initialize();

            _photonManager.PlayerReady();
        }


        public override void Deinitialize() {

        }

    }
}
