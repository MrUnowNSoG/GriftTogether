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

            _mapManager = new MapManager(_mainCanvas, _playgroundManager, _localServiceLocator);
            _rpcManager = new MapPhotonRPCManager(_localServiceLocator, _mapManager, _overlayCanvas);

            MapPlayerObject player = _photonManager.Initialize(_localServiceLocator, _playgroundManager);
            
            _playgroundManager.Initialize(_mapManager, player);

            _mapManager.Initialize(player);

            _rpcManager.Initialize();

            _photonManager.PlayerReady();
        }


        public override void Deinitialize() {

        }

    }
}
